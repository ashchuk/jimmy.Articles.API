using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using jimmy.Articles.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace jimmy.Articles.API.Context
{
    public class ArticlesDatabaseInMemoryDbContext: DbContext, IArticlesDatabaseContext
    {
        private IDbContextTransaction _currentTransaction;

        public ArticlesDatabaseInMemoryDbContext(DbContextOptions<ArticlesDatabaseInMemoryDbContext> options)
            : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public new Task SaveChangesAsync(CancellationToken cancellationToken) =>
            base.SaveChangesAsync(cancellationToken);
        
        public Task SaveChangesAsync() =>
            base.SaveChangesAsync();
        
        public new DbSet<T> Set<T>([NotNull] string name) where T : class =>
            base.Set<T>(name);      
        
        public new DbSet<T> Set<T>() where T : class =>
            base.Set<T>();

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();

                await (_currentTransaction?.CommitAsync() ?? Task.CompletedTask);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}