using System;

namespace CleanCoders
{
    public class Codecast : Entity
    {
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public string Permalink { get; set; }
    }
}