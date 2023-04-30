using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Theses.Application.Common.Interfaces;
using Theses.Domain.Entities;

namespace Theses.Application.Theses.Commands.Update;

public record UpdateThesisCommand(long Id, Person MainAuthor, string ContactEmail, ICollection<Person>? OtherAuthors,
    string Topic, string Content) : IRequest<Result<Thesis>>;

public record UpdateThesisCommandHandler : IRequestHandler<UpdateThesisCommand, Result<Thesis>>
{
    private readonly IApplicationContext _context;

    public UpdateThesisCommandHandler(IApplicationContext context) => _context = context;

    public async Task<Result<Thesis>> Handle(UpdateThesisCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Theses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null) return Result.Fail<Thesis>("Thesis not found");

        entity.MainAuthor = request.MainAuthor;
        entity.ContactEmail = request.ContactEmail;
        entity.OtherAuthors = request.OtherAuthors;
        entity.Topic = request.Topic;
        entity.Content = request.Content;
        entity.Updated = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok(entity);
    }
}
