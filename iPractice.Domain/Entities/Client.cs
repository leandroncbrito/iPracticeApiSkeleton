using System.Collections.Generic;

namespace iPractice.Domain.Entities
{
    public class Client
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Psychologist> Psychologists { get; set; }
    }
}