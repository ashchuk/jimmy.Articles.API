using System;
using jimmy.Articles.API.Models;
using MediatR;

namespace jimmy.Articles.API.Domain.Articles.Commands
{
    public class UpdateArticleCommand: IRequest<Article>
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public UpdateArticleCommand(Guid id, string title, string body)
        {
            this.Id = id;
            this.Title = title;
            this.Body = body;
        }
    }
}