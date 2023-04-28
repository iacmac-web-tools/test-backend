using Theses.Api.Mappings.Person;

namespace Theses.Api.Mappings.Create;

public record CreateThesisDto(PersonDto MainAuthor, string ContactEmail, ICollection<PersonDto>? OtherAuthors, string Topic,
    string Content);
