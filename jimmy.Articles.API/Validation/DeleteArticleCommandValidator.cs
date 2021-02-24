using FluentValidation;
using jimmy.Articles.API.Domain.Articles.Commands;

namespace jimmy.Articles.API.Validation
{
    public class DeleteArticleCommandValidator: AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator()
        {
            RuleFor(resource =>  resource.Id).NotNull();
        }
    }
}
