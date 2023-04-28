namespace Theses.Domain.Entities;

public record Thesis(Person MainAuthor, string ContactEmail, ICollection<Person>? OtherAuthors, string Topic, string Content)
{
    public long Id { get; }
    public Person MainAuthor { get; set; } = MainAuthor;
    public string ContactEmail { get; set; } = ContactEmail;
    public ICollection<Person>? OtherAuthors { get; set; } = OtherAuthors;
    public string Topic { get; set; } = Topic;
    public string Content { get; set; } = Content;
    public DateTime Created { get; } = DateTime.UtcNow;
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}
