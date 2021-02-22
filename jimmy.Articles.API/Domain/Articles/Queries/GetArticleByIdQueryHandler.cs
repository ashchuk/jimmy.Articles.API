using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using jimmy.Articles.API.Context;
using jimmy.Articles.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace jimmy.Articles.API.Domain.Articles.Queries
{
    public class GetArticleByIdQueryHandler: IRequestHandler<GetArticleByIdQuery, Article>
    {
        private readonly ArticlesDatabaseContext _context;

        public GetArticleByIdQueryHandler(ArticlesDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Article> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles
                .Where(item => item.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return article;
        }
    }
}