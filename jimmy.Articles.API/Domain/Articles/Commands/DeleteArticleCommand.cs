using System;
using jimmy.Articles.API.Models;
using MediatR;

namespace jimmy.Articles.API.Domain.Articles.Commands
{
    public class DeleteArticleCommand : IRequest<Article>
    {
        public Guid Id { get; private set; }

        public DeleteArticleCommand(Guid id)
        {
            this.Id = id;
        }
    }
}