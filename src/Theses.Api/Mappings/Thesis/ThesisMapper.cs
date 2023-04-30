using Riok.Mapperly.Abstractions;
using Theses.Application.Common.Models;

namespace Theses.Api.Mappings.Thesis;

[Mapper]
public partial class ThesisMapper
{
    public partial ThesisDto ThesisToThesisDto(Domain.Entities.Thesis thesis);
    public partial PaginatedList<ThesisDto> PaginatedThesesToPaginatedThesesDto(PaginatedList<Domain.Entities.Thesis> result);

    public partial IEnumerable<ThesisDto> ThesesToThesesDto(IEnumerable<Domain.Entities.Thesis> theses);
}
