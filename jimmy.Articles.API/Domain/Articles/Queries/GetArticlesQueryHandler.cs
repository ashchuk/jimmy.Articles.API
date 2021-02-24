using System;
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
            if (request.Limit < 0)
            {
                // throw new ArgumentException("The limit parameter shout be greater than zero!");
                return new List<Article>();
            }
            if (request.Offset < 0)
            {
                // throw new ArgumentException("The offset parameter shout be greater than zero!");
                return new List<Article>();
            }

            var articleQuery = _context.Articles.AsQueryable();
            if (request.DescendingOrderFlag)
            {
                articleQuery = articleQuery.OrderByDescending(item => item.CreationDate);
            }
            articleQuery = articleQuery.Skip(request.Offset).Take(request.Limit);

            return await articleQuery.ToListAsync(cancellationToken);
        }
    }
}