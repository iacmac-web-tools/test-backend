using FluentValidation;

namespace Theses.Application.Theses.Queries.GetPaginatedList;

public class GetPaginatedListQueryValidator : AbstractValidator<GetPaginatedListQuery>
{
    public GetPaginatedListQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}
