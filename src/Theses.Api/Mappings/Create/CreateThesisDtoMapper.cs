using Riok.Mapperly.Abstractions;
using Theses.Application.Theses.Commands.Create;

namespace Theses.Api.Dtos.Create;

[Mapper]
public partial class CreateThesisDtoMapper
{
    public partial CreateThesisCommand CreateThesisDtoToCreateThesisCommand(CreateThesisDto createThesisDto);
}
