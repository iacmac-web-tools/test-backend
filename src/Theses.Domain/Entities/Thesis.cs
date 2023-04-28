namespace Theses.Domain.Entities;

public record Thesis
{
    public long Id { get; }
    public Person MainAuthor { get; set; }
    public string ContactEmail { get; set; }
    public ICollection<Person>? OtherAuthors { get; set; } = new List<Person>();
    public string Topic { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    public Thesis(Person mainAuthor, string contactEmail, ICollection<Person>? otherAuthors, string topic, string content)
    {
        MainAuthor = mainAuthor;
        ContactEmail = contactEmail;
        OtherAuthors = otherAuthors;
        Topic = topic;
        Content = content;
        Created = DateTime.UtcNow;
        Updated = DateTime.UtcNow;
    }

    private Thesis() { }
}
