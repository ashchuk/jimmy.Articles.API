using FluentValidation;
using jimmy.Articles.API.Domain.Articles.Queries;

namespace jimmy.Articles.API.Validation
{
    public class GetArticlesQueryValidator: AbstractValidator<GetArticlesQuery>
    {
        public GetArticlesQueryValidator()
        {
            RuleFor(resource =>  resource.Limit).GreaterThan(0);
            RuleFor(resource =>  resource.Offset).GreaterThan(-1);
        }
    }
}