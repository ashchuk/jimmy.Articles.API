using System.Collections.Generic;
using jimmy.Articles.API.Models;
using MediatR;

namespace jimmy.Articles.API.Domain.Articles.Queries
{
    public class GetArticlesQuery: IRequest<List<Article>>
    {
        public int Limit { get; private set; }
        public int Offset { get; private set; }
        public bool DescendingOrderFlag { get; private set; }

        public GetArticlesQuery(int limit,int offset, bool descending)
        {
            this.Limit = limit;
            this.Offset = offset;
            this.DescendingOrderFlag = descending;
        }
    }
}