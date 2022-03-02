using System.ComponentModel.DataAnnotations;

namespace BackTask.Models
{
    public class PersonResource
    {
        public int id { get; set; }

        [StringLength(255)]
        public string firstName { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string lastName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? middleName { get; set; } = string.Empty;

        [StringLength(500)]
        public string workplace { get; set; } = string.Empty;
    }
}
