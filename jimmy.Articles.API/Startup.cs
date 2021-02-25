using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using FluentValidation;
using jimmy.Articles.API.Context;
using jimmy.Articles.API.Infrastructure.Auth;
using jimmy.Articles.API.PipelineBehaviors;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
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
            #region Database
            var databaseSection = Configuration.GetSection("Database");
            var databaseSettings = databaseSection?.Get<DatabaseSettings>();
            services.Configure<DatabaseSettings>(databaseSection);

            if (databaseSettings?.DatabaseType == "SQLServer")
            {
                services.AddEntityFrameworkSqlServer()
                    .AddDbContext<IArticlesDatabaseContext, ArticlesDatabaseSqlServerContext>((serviceProvider, options) =>
                    {
                        options.UseSqlServer(Configuration["ConnectionString"],
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            });
                        options.UseInternalServiceProvider(serviceProvider);
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
            #endregion
            #region Swagger
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
            #endregion
            services.AddMediatR(typeof(Startup).Assembly);
            
            // Logging and Validation app behavior
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddScoped<IUserService, UserService>();
            
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IArticlesDatabaseContext articlesContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Database
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
            #endregion
            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Articles API V1");
            });
            #endregion
            #region ValidationExceptionsPreprocessing
            // Workaround for validation exceptions thrown by ValidationBehavior
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;
 
                    // https://tools.ietf.org/html/rfc7807#section-3.1
                    var problemDetails = new ProblemDetails
                    {
                        Type = $"https://example.com/problem-types/{exception.GetType().Name}",
                        Title = "An unexpected error occurred!",
                        Detail = "Something went wrong",
                        Instance = errorFeature switch
                        {
                            ExceptionHandlerFeature e => e.Path,
                            _ => "unknown"
                        },
                        Status = StatusCodes.Status400BadRequest,
                        Extensions =
                        {
                            ["trace"] = Activity.Current?.Id ?? context?.TraceIdentifier
                        }
                    };
 
                    switch (exception)
                    {
                        case ValidationException validationException:
                            problemDetails.Status = StatusCodes.Status403Forbidden;
                            problemDetails.Title = "One or more validation errors occurred";
                            problemDetails.Detail = "The request contains invalid parameters. More information can be found in the errors.";
                            problemDetails.Extensions["errors"] = validationException.Errors;
                            break;
                    }
                    
                    // TODO: add XML serializer switcher
                    context.Response.ContentType = "application/problem+json";
                    context.Response.StatusCode = problemDetails.Status.Value;
                    context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                    {
                        NoCache = true,
                    };
                    
                    // TODO: add XML serializer switcher
                    await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails);
                });
            });
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}