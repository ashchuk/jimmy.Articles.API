using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using jimmy.Articles.API.Context;
using jimmy.Articles.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace jimmy.Articles.API.Domain.Articles.Commands
{
    public class UpdateArticleCommandHandler: IRequestHandler<UpdateArticleCommand, Article>
    {
        private readonly ArticlesDatabaseContext _context;

        public UpdateArticleCommandHandler(ArticlesDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Article> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles
                .Where(item => item.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (article == null) return default;

            article.Title = request.Title;
            article.Body = request.Body;
            article.UpdatingDate = DateTime.Now;
            
            _context.Articles.Update(article);
            await _context.SaveChangesAsync(cancellationToken);
            return article;
        }
    }
}