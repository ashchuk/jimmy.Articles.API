using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using jimmy.Articles.API.Models;
using Microsoft.EntityFrameworkCore;

namespace jimmy.Articles.API.Context
{
    public interface IArticlesDatabaseContext
    {
        DbSet<Article> Articles { get; set; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task SaveChangesAsync();
        DbSet<T> Set<T>([NotNull] string name) where T : class;
        DbSet<T> Set<T>() where T : class;
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}