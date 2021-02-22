using jimmy.Articles.API.Models;
using Microsoft.EntityFrameworkCore;

namespace jimmy.Articles.API.Context
{
    public class ArticlesDatabaseContext: DbContext, IArticlesDatabaseContext
    {
        public ArticlesDatabaseContext(DbContextOptions<ArticlesDatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
    }
}