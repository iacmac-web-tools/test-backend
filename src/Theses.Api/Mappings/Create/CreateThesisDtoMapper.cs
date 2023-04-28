using Riok.Mapperly.Abstractions;
using Theses.Application.Theses.Commands.Create;

namespace Theses.Api.Mappings.Create;

[Mapper]
public partial class CreateThesisDtoMapper
{
    public partial CreateThesisCommand CreateThesisDtoToCreateThesisCommand(CreateThesisDto createThesisDto);
}
