namespace BackTask.Models
{
    public class ThesisResource: ThesisForm
    {
        public int Id { get; set; }
        
        public DateTime created { get; set; }
        
        public DateTime updated { get; set; }
    }
}
