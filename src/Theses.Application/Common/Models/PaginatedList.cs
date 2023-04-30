using Gridify;
using Gridify.EntityFramework;
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

    public static Task<PaginatedList<T>> Create(Paging<T> paging, int pageNumber, int pageSize, int totalItems)
    {
        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        var items = paging.Data as IReadOnlyCollection<T> ?? paging.Data.ToList();

        return Task.FromResult(new PaginatedList<T>(totalItems, pageNumber, pageSize, totalPages, items));
    }
}

public static class PaginatedListExtensions
{
    public async static Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> queryable, int pageNumber,
        int pageSize) => await PaginatedList<T>.CreateAsync(queryable, pageNumber, pageSize);

    public async static Task<PaginatedList<T>> ToPaginatedList<T>(this Paging<T> paging, int pageNumber, int pageSize, int totalItems) =>
        await PaginatedList<T>.Create(paging, pageNumber, pageSize, totalItems);

    public async static Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> queryable,
        GridifyQuery query, int pageNumber, int pageSize) =>
        await (await queryable.GridifyAsync(query)).ToPaginatedList(pageNumber, pageSize, await queryable.CountAsync());
}
