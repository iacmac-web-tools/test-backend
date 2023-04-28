using Riok.Mapperly.Abstractions;

namespace Theses.Api.Dtos.Thesis;

[Mapper]
public partial class ThesisMapper
{
    public partial ThesisDto ThesisToThesisDto(Domain.Entities.Thesis thesis);
}
