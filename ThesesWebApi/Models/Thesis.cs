namespace ThesesWebApi.Models
{
    public class Thesis
    {
        public int Id { get; set; }
        public Person MainAuthor { get; set; }
        public string ContactEmail { get; set; }
        public ICollection<Person>? OtherAuthors { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
