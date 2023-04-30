using FluentValidation;

namespace Theses.Application.Theses.Commands.Update;

public class UpdateThesisCommandValidator : AbstractValidator<UpdateThesisCommand>
{
    public UpdateThesisCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.MainAuthor).NotNull();
        RuleFor(x => x.ContactEmail).EmailAddress();
        RuleFor(x => x.Topic).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Content).NotEmpty().MaximumLength(5000);
    }
}
