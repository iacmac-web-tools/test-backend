namespace Theses.Domain.Entities;

public record Person(string FirstName, string MiddleName, string LastName, string Workplace)
{
    public Person(string FirstName, string LastName, string Workplace)
        : this(FirstName, string.Empty, LastName, Workplace) { }

    public long Id { get; }
    public string FirstName { get; set; } = FirstName;
    public string? MiddleName { get; set; } = MiddleName;
    public string LastName { get; set; } = LastName;
    public string Workplace { get; set; } = Workplace;
}
