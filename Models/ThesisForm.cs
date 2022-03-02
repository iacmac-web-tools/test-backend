using System.ComponentModel.DataAnnotations;

namespace BackTask.Models
{
    public class ThesisForm
    {
        public PersonResource mainAuthor { get; set; } = new PersonResource();

        [StringLength(255)]
        public string? contactEmail { get; set; } = string.Empty;
        public List<PersonResource>? otherAuthors { get; set; } = new List<PersonResource>();

        [StringLength(500)]
        public string topic { get; set; } = string.Empty;

        [StringLength(5000)]
        public string? content { get; set; } = String.Empty;
    }
}
