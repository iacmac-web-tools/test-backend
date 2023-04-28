using Theses.Api.Dtos.Person;

namespace Theses.Api.Dtos.Create;

public record CreateThesisDto(PersonDto MainAuthor, string ContactEmail, ICollection<PersonDto>? OtherAuthors, string Topic,
    string Content);
