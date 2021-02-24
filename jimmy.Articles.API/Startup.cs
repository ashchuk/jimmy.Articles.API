using System;
using System.Linq;
using System.Reflection;
using jimmy.Articles.API.Context;
using jimmy.Articles.API.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace jimmy.Articles.API
{
    public class DatabaseSettings
    {
        public string DatabaseType { get; set; }
    }
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var databaseSection = Configuration.GetSection("Database");
            var databaseSettings = databaseSection?.Get<DatabaseSettings>();
            services.Configure<DatabaseSettings>(databaseSection);

            if (databaseSettings?.DatabaseType == "SQLServer")
            {
                services.AddEntityFrameworkSqlServer()
                    .AddDbContext<IArticlesDatabaseContext, ArticlesDatabaseSqlServerContext>(options =>
                    {
                        options.UseSqlServer(Configuration["ConnectionString"],
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            });

                        // Changing default behavior when client evaluation occurs to throw. 
                        // Default in EF Core would be to log a warning when client evaluation is performed.
                        // RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning == CoreEventId.RelationalBaseId + 500
                        options.ConfigureWarnings(warnings => warnings.Throw(CoreEventId.RelationalBaseId + 500));
                        //Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
                    });
            }
            else
            {
                services.AddDbContext<IArticlesDatabaseContext, ArticlesDatabaseInMemoryDbContext>(options =>
                {
                    options.UseInMemoryDatabase("Articles");
                });
            }
            
            services.AddMediatR(typeof(Startup));
            
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddScoped<IUserService, UserService>();
            
            services.AddSwaggerGen();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
 
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] {}
                    }
                });
                
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Articles API",
                    Description = "A simple Articles CMS system written in ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Artyom Koshko",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/ashchuk"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IArticlesDatabaseContext articlesContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var databaseSection = Configuration.GetSection("Database");
            var databaseSettings = databaseSection?.Get<DatabaseSettings>();
            if (databaseSettings?.DatabaseType == "SQLServer")
            {
                var context = (DbContext) articlesContext;
                if (context != null && context.Database.GetPendingMigrations().Any())
                {
                    context.Database.EnsureCreated();
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Articles API V1");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}