using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Theses.Application.Common.Interfaces;
using Theses.Domain.Entities;

namespace Theses.Application.Theses.Queries.Get;

public record GetThesisQuery(long Id) : IRequest<Result<Thesis>>;

public class GetThesisQueryHandler : IRequestHandler<GetThesisQuery, Result<Thesis>>
{
    private readonly IApplicationContext _context;
    public GetThesisQueryHandler(IApplicationContext context) => _context = context;

    public async Task<Result<Thesis>> Handle(GetThesisQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Theses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return entity is null
            ? Result.Fail<Thesis>("Thesis not found")
            : Result.Ok(entity);
    }
}
