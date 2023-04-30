using Theses.Api.Mappings.Person;

namespace Theses.Api.Mappings.Thesis;

public record ThesisDto(long Id, PersonDto MainAuthor, string ContactEmail, IReadOnlyCollection<PersonDto>? OtherAuthors,
    string Topic, string Content, DateTime Created, DateTime Updated);
