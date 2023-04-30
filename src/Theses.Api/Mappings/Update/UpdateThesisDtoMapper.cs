using Theses.Api.Mappings.Person;
using Theses.Application.Theses.Commands.Update;

namespace Theses.Api.Mappings.Update;

public partial class UpdateThesisDtoMapper
{
    public UpdateThesisCommand UpdateThesisDtoToUpdateThesisCommand(long id, UpdateThesisDto dto)
    {
        var personMapper = new PersonMapper();
        var otherAuthors = dto.OtherAuthors.Select(x => personMapper.PersonDtoToPerson(x)).ToList();
        return new UpdateThesisCommand(id,
            personMapper.PersonDtoToPerson(dto.MainAuthor),
            dto.ContactEmail,
            otherAuthors,
            dto.Topic,
            dto.Content);
    }
}
