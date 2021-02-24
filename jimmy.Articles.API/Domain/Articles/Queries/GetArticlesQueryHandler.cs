using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using jimmy.Articles.API.Context;
using jimmy.Articles.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace jimmy.Articles.API.Domain.Articles.Queries
{
    public class GetArticlesQueryHandler: IRequestHandler<GetArticlesQuery, List<Article>>
    {
        private readonly IArticlesDatabaseContext _context;

        public GetArticlesQueryHandler(IArticlesDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Article>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articleQuery = _context.Articles
                .Take(request.Limit);
            if (request.DescendingOrderFlag)
            {
                articleQuery = articleQuery.OrderByDescending(item => item.CreationDate);
            }
            return await articleQuery.ToListAsync(cancellationToken);
        }
    }
}