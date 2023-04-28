using Theses.Api.Dtos.Person;

namespace Theses.Api.Dtos.Thesis;

public record ThesisDto(long Id, PersonDto MainAuthor, string ContactEmail, IReadOnlyCollection<PersonDto>? OtherAuthors,
    string Topic, string Content, DateTime Created, DateTime Updated);
