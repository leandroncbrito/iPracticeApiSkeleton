using System;

namespace iPractice.Domain.Entities
{
    public class Availability
    {
        public long Id { get; set; }
        
        public DateTime From { get; set; }
        
        public DateTime To { get; set; }

        public Psychologist Psychologist { get; set; }
    }
}