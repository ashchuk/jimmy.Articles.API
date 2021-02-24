using FluentValidation;
using jimmy.Articles.API.Domain.Articles.Commands;

namespace jimmy.Articles.API.Validation
{
    public class CreateArticleCommandValidator: AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(resource =>  resource.Body).NotNull();
            RuleFor(resource =>  resource.Body).MaximumLength(500);
            RuleFor(resource =>  resource.Title).NotNull();
            RuleFor(resource =>  resource.Title).MaximumLength(100);
            RuleFor(resource =>  resource.Title).MinimumLength(1);
        }
    }
}
