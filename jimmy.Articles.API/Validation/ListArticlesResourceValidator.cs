using FluentValidation;
using jimmy.Articles.API.Infrastructure.Communication;

namespace jimmy.Articles.API.Validation
{
    public class ListArticlesResourceValidator : AbstractValidator<ListArticlesResource>
    {
        public ListArticlesResourceValidator()
        {
            RuleFor(resource =>  resource.Limit).GreaterThan(1);
            RuleFor(resource =>  resource.Offset).GreaterThan(1);
            RuleFor(resource =>  resource.OrderByDescending).NotNull();
        }
    }
}