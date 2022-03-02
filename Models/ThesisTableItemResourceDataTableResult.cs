namespace BackTask.Models
{
    public class ThesisTableItemResourceDataTableResult
    {
        public int totalItems { get; set; }
        
        public int page { get; set; }
        
        public int pageSize { get; set; }
        
        public int totalPages { get; set; }
        
        public List<ThesisTableItemResource>? items { get; set; }
    }
}
