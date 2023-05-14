﻿namespace ThesesWebApi.Models
{
    public class ThesisTable
    {
        public int Id { get; set; }
        public string MainAuthor { get; set; }
        public string ContactEmail { get; set; }
        public string Topic { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
