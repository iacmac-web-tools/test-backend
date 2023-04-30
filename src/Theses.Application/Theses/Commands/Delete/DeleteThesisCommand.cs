using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Theses.Application.Common.Interfaces;

namespace Theses.Application.Theses.Commands.Delete;

public record DeleteThesisCommand(long Id) : IRequest<Result>;

public class DeleteThesisCommandHandler : IRequestHandler<DeleteThesisCommand, Result>
{
    private readonly IApplicationContext _context;

    public DeleteThesisCommandHandler(IApplicationContext context) => _context = context;

    public async Task<Result> Handle(DeleteThesisCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Theses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (entity is null) return Result.Fail("Thesis not found");

        _context.Theses.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}
