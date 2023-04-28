using FluentValidation;

namespace Theses.Application.Theses.Queries.Get;

public class GetThesisQueryValidator : AbstractValidator<GetThesisQuery>
{
    public GetThesisQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
