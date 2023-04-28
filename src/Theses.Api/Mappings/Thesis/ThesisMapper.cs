using Riok.Mapperly.Abstractions;

namespace Theses.Api.Mappings.Thesis;

[Mapper]
public partial class ThesisMapper
{
    public partial ThesisDto ThesisToThesisDto(Domain.Entities.Thesis thesis);
}
