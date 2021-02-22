using jimmy.Articles.API.Models;
using Microsoft.EntityFrameworkCore;

namespace jimmy.Articles.API.Context
{
    public interface IArticlesDatabaseContext
    {
        
        DbSet<Article> Products { get; set; }
    }
}