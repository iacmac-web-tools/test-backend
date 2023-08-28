using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesesDomain.Models
{
    public class PersonResource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? MiddleName { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Workplace { get; set; } = string.Empty;
    }
}