using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesesDomain.Models
{
    public class ThesisForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public PersonResource mainAuthor { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string? contactEmail { get; set; } = string.Empty;
        public List<PersonResource>? otherAuthors { get; set; } = null!;
        [Required]
        [StringLength(500)]
        public string topic { get; set; } = string.Empty;
        [Required]
        [StringLength(5000)]
        public string? content { get; set; } = string.Empty;
    }
}
