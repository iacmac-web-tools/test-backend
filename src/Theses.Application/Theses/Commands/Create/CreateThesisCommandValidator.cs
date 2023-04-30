using FluentValidation;

namespace Theses.Application.Theses.Commands.Create;

public class CreateThesisCommandValidator : AbstractValidator<CreateThesisCommand>
{
    public CreateThesisCommandValidator()
    {
        RuleFor(x => x.MainAuthor).NotNull();
        RuleFor(x => x.ContactEmail).EmailAddress();
        RuleFor(x => x.Topic).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Content).NotEmpty().MaximumLength(5000);
    }
}
