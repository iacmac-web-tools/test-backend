using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ThesesDomain.Models
{
    public class ThesisResource
    {
        public int? id { get; set; }
        public PersonResource? mainAuthor { get; set; }
        public string? contactEmail { get; set; } = string.Empty;
        public PersonResource? otherAuthors { get; set; }
        public string? topic { get; set; } = string.Empty;
        public string? content { get; set; } = string.Empty;
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
    }
}
