using System;

namespace iPractice.Api.Models
{
    public class Availability
    {
        public long Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}