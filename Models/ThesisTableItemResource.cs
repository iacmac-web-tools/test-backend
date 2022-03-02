namespace BackTask.Models
{
    public class ThesisTableItemResource
    {
        public int Id { get; set; }

        public string? mainAuthor { get; set; } = string.Empty;

        public string? contactEmail { get; set; } = string.Empty;

        public string? topic { get; set; } = string.Empty;
        
        public DateTime created { get; set; }
        
        public DateTime updated { get; set; }
    }
}
