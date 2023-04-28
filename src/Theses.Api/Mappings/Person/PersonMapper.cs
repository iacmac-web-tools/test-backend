using Riok.Mapperly.Abstractions;

namespace Theses.Api.Dtos.Person;

[Mapper]
public partial class PersonMapper
{
    public partial PersonDto PersonToPersonDto(Domain.Entities.Person person);
}
