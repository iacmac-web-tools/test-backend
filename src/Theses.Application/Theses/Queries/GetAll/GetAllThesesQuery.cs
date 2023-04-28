using MediatR;
using Microsoft.EntityFrameworkCore;
using Theses.Application.Common.Interfaces;
using Theses.Domain.Entities;

namespace Theses.Application.Theses.Queries.GetAll;

public record GetAllThesesQuery : IRequest<IReadOnlyCollection<Thesis>>
{
    public readonly static GetAllThesesQuery Instance = new();
};

public class GetAllThesesQueryHandler : IRequestHandler<GetAllThesesQuery, IReadOnlyCollection<Thesis>>
{
    private readonly IApplicationContext _context;

    public GetAllThesesQueryHandler(IApplicationContext context) => _context = context;

    public async Task<IReadOnlyCollection<Thesis>> Handle(GetAllThesesQuery request, CancellationToken cancellationToken) =>
        await _context.Theses.Include(x => x.MainAuthor).Include(x => x.OtherAuthors).ToArrayAsync(cancellationToken);
}
