using FluentValidation;

namespace Theses.Application.Theses.Commands.Delete;

public class DeleteThesisCommandValidator : AbstractValidator<DeleteThesisCommand>
{
    public DeleteThesisCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
