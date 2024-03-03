using iPractice.Domain.Exceptions;

namespace iPractice.Domain.Entities
{
    public class Psychologist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Client> Clients { get; set; } = new ();
        public List<TimeSlot> TimeSlots { get; set; } = new ();
        
        public void AddTimeSlots(IEnumerable<TimeSlot> timeSlots)
        {
            TimeSlots.AddRange(timeSlots);
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