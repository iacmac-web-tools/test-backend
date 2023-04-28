namespace Theses.Domain.Entities;

public record Person
{
    public long Id { get; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Workplace { get; set; }

    public Person(string firstName, string? middleName, string lastName, string workplace)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Workplace = workplace;
    }

    public Person(string firstName, string lastName, string workplace)
    {
        FirstName = firstName;
        LastName = lastName;
        Workplace = workplace;
    }

    private Person() { }
}
