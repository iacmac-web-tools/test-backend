using Riok.Mapperly.Abstractions;

namespace Theses.Api.Mappings.Person;

[Mapper]
public partial class PersonMapper
{
    public partial PersonDto PersonToPersonDto(Domain.Entities.Person person);
    public partial Domain.Entities.Person PersonDtoToPerson(PersonDto person);
}
