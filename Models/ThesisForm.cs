using System.ComponentModel.DataAnnotations;

namespace BackTask.Models
{
    public class ThesisForm
    {
        [Required]
        public PersonResource mainAuthor{ get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string? contactEmail { get; set; } = string.Empty;
        
        public List<PersonResource>? otherAuthors { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string topic { get; set; } = string.Empty;

        [Required]
        [StringLength(5000)]
        public string? content { get; set; } = String.Empty;
    }
}
