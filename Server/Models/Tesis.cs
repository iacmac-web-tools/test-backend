using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Server.Models
{
    public class Tesis
    {
        public int? id { get; set; }

        public string? autor { get; set; }
        public string? autorEmail { get; set; }
        public string? autorWork { get; set; }
        public string? co_autor { get; set; }
        public string? body { get; set; }
        public string? head { get; set; }
        public Tesis() { }
        public Tesis(string head, string body, string autor, string co_autor = " ", int id = 0)
        {
            this.autor = autor;
            this.co_autor = co_autor;
            this.body = body;
            this.head = head;
            this.id = id;
        }
    }
}
