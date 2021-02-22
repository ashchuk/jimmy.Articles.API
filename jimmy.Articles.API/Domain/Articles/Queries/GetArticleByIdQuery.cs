using System;
using jimmy.Articles.API.Models;
using MediatR;

namespace jimmy.Articles.API.Domain.Articles.Queries
{
    public class GetArticleByIdQuery : IRequest<Article>
    {
        public Guid Id { get; private set; }

        public GetArticleByIdQuery(Guid id)
        {
            this.Id = id;
        }
    }
}