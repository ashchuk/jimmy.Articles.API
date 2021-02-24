using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using jimmy.Articles.API.Context;
using jimmy.Articles.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace jimmy.Articles.API.Domain.Articles.Commands
{
    public class DeleteArticleCommandHandler: IRequestHandler<DeleteArticleCommand, Article>
    {
        private readonly IArticlesDatabaseContext _context;

        public DeleteArticleCommandHandler(IArticlesDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Article> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles
                .Where(item => item.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            if (article == null) return default;
            
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync(cancellationToken);
            return article;
        }
    }
}