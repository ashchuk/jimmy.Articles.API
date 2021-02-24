using FluentValidation;
using jimmy.Articles.API.Domain.Articles.Queries;

namespace jimmy.Articles.API.Validation
{
    public class GetArticleByIdQueryValidator: AbstractValidator<GetArticleByIdQuery>
    {
        public GetArticleByIdQueryValidator()
        {
            RuleFor(resource =>  resource.Id).NotNull();
        }
    }
}
