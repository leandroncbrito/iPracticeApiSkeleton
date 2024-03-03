using iPractice.Domain.Exceptions;

namespace iPractice.Domain.Entities
{
    public class Psychologist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Client> Clients { get; set; } = new ();
        public List<Availability> Availabilities { get; set; } = new ();
        
        public void AddAvailability(IEnumerable<Availability> timeSlots)
        {
            Availabilities.AddRange(timeSlots);
        }

        public void Validate(long psychologistId)
        {
            if (Id != psychologistId)
            {
                throw new PsychologistMismatchException();
            }
        }
    }
}