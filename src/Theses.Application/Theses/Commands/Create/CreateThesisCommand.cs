using FluentResults;
using MediatR;
using Theses.Application.Common.Interfaces;
using Theses.Domain.Entities;

namespace Theses.Application.Theses.Commands.Create;

public record CreateThesisCommand(Person MainAuthor, string ContactEmail, ICollection<Person>? OtherAuthors, string Topic,
    string Content) : IRequest<Result<Thesis>>;

public class CreateThesisCommandHandler : IRequestHandler<CreateThesisCommand, Result<Thesis>>
{
    private readonly IApplicationContext _context;

    public CreateThesisCommandHandler(IApplicationContext context) => _context = context;

    public async Task<Result<Thesis>> Handle(CreateThesisCommand request, CancellationToken cancellationToken)
    {
        var entity = new Thesis(request.MainAuthor, request.ContactEmail, request.OtherAuthors, request.Topic, request.Content);
        await _context.Theses.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok(entity);
    }
}
