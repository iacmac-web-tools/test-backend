using Microsoft.EntityFrameworkCore;

namespace Theses.Application.Common.Models;

public record PaginatedList<T>(int TotalItems, int PageNumber, int PageSize, int TotalPages, IReadOnlyCollection<T> Items)
{
    public async static Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        int count = await source.CountAsync();
        int totalPages = (int)Math.Ceiling((double)count / pageSize);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(count, pageNumber, pageSize, totalPages, items);
    }
}

public static class PaginatedListExtensions
{
    public async static Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> queryable, int pageNumber,
        int pageSize) => await PaginatedList<T>.CreateAsync(queryable, pageNumber, pageSize);
}
