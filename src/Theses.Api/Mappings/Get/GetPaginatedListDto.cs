namespace Theses.Api.Mappings.Get;

public record GetPaginatedListDto(int PageNumber, int PageSize, string? OrderBy, string? Filter);
