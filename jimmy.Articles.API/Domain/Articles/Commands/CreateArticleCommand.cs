using jimmy.Articles.API.Models;
using MediatR;

namespace jimmy.Articles.API.Domain.Articles.Commands
{
    public class CreateArticleCommand : IRequest<Article>
    {
        public string Title { get; private set; }
        public string Body { get; private set; }

        public CreateArticleCommand(string title, string body)
        {
            this.Title = title;
            this.Body = body;
        }
    }
}