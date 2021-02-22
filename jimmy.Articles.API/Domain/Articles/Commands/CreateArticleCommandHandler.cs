using System;
using System.Threading;
using System.Threading.Tasks;
using jimmy.Articles.API.Context;
using jimmy.Articles.API.Models;
using MediatR;

namespace jimmy.Articles.API.Domain.Articles.Commands
{
    public class CreateArticleCommandHandler: IRequestHandler<CreateArticleCommand, Article>
    {
        private readonly ArticlesDatabaseContext _context;

        public CreateArticleCommandHandler(ArticlesDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Article> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = new Article
            {             
                Id = Guid.NewGuid(),
                Title = request.Title,
                Body = request.Body,
                CreationDate = DateTime.Now,
                UpdatingDate = DateTime.Now
            };

            await _context.Articles.AddAsync(article, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return article;
        }
    }
}