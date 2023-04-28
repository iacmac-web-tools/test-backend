using Theses.Api.Mappings.Person;

namespace Theses.Api.Mappings.Update;

public record UpdateThesisDto(PersonDto MainAuthor, string ContactEmail, IReadOnlyCollection<PersonDto> OtherAuthors,
    string Topic, string Content);
