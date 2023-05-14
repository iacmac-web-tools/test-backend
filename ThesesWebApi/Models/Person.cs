using System.ComponentModel.DataAnnotations;

namespace ThesesWebApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string LastName { get; set; }
        [MaxLength(50), MinLength(2)]
        public string? MiddleName { get; set; }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string WorkPlace { get; set; }
    }
}
