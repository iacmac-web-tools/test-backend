using System.ComponentModel.DataAnnotations;
using ThesesWebApi.Models;

namespace ThesesWebApi.Dto
{
    public class ThesisDto
    {
        [Required]
        public Person MainAuthor { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Please, enter a valid Email")]
        public string ContactEmail { get; set; }
        public ICollection<Person>? OtherAuthor { get; set; }
        [Required]
        [MaxLength(500)]
        public string Topic { get; set; }
        [Required]
        [MaxLength(5000)]
        public string Content { get; set; }
    }
}
