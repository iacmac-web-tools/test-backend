using Riok.Mapperly.Abstractions;
using Theses.Application.Theses.Queries.GetPaginatedList;

namespace Theses.Api.Mappings.Get;

[Mapper]
public partial class GetPaginatedListDtoMapper
{
    public partial GetPaginatedListQuery GetPaginatedListDtoToGetPaginatedListQuery(GetPaginatedListDto dto);
}
