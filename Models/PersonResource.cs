using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BackTask.Models
{
    [Owned]
    public class PersonResource
    {
        // public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string firstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string lastName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? middleName { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string workplace { get; set; } = string.Empty;
    }
}
