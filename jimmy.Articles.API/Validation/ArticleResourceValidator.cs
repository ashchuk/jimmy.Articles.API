using FluentValidation;
using jimmy.Articles.API.Infrastructure.Communication;

namespace jimmy.Articles.API.Validation
{
    public class ArticleResourceValidator : AbstractValidator<ArticleResource>
    {
        public ArticleResourceValidator()
        {
            RuleFor(resource =>  resource.Body).NotNull();
            RuleFor(resource =>  resource.Body).MaximumLength(500);
            RuleFor(resource =>  resource.Title).NotNull();
            RuleFor(resource =>  resource.Title).MaximumLength(100);
            RuleFor(resource =>  resource.Title).MinimumLength(1);
        }
    }
}