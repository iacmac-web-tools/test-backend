using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Theses.Application.Common.Interfaces;
using Theses.Application.Common.Models;
using Theses.Domain.Entities;

namespace Theses.Application.Theses.Queries.GetPaginatedList;

public record GetPaginatedListQuery(int PageNumber, int PageSize, string? OrderBy, string? Filter) : IRequest<PaginatedList<Thesis>>;

public class GetPaginatedListQueryHandler : IRequestHandler<GetPaginatedListQuery, PaginatedList<Thesis>>
{
    private readonly IApplicationContext _context;

    public GetPaginatedListQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Thesis>> Handle(GetPaginatedListQuery request, CancellationToken cancellationToken)
    {
        var query = new GridifyQuery
        {
            Page = request.PageNumber,
            PageSize = request.PageSize,
            OrderBy = request.OrderBy,
            Filter = request.Filter
        };
        return await _context.Theses
            .Include(x => x.MainAuthor)
            .Include(x => x.OtherAuthors)
            .ToPaginatedListAsync(query, request.PageNumber, request.PageSize);
    }
}
