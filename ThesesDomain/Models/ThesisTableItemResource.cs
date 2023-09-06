using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesesDomain.Models
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
