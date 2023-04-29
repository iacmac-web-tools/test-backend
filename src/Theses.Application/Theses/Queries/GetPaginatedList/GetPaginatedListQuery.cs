using MediatR;
using Microsoft.EntityFrameworkCore;
using Theses.Application.Common.Interfaces;
using Theses.Application.Common.Models;
using Theses.Domain.Entities;

namespace Theses.Application.Theses.Queries.GetPaginatedList;

public record GetPaginatedListQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<Thesis>>;

public class GetPaginatedListQueryHandler : IRequestHandler<GetPaginatedListQuery, PaginatedList<Thesis>>
{
    private readonly IApplicationContext _context;

    public GetPaginatedListQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Thesis>> Handle(GetPaginatedListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Theses
            .Include(x => x.MainAuthor)
            .Include(x => x.OtherAuthors)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
