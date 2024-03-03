using iPractice.Domain.Exceptions;

namespace iPractice.Domain.Entities
{
    public class TimeSlot
    {
        public TimeSlot(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
        
        public long Id { get; private set; }
        
        public DateTime From { get; private set; }
        
        public DateTime To { get; private set; }

        public Psychologist Psychologist { get; private set; }
        
        public Client Client { get; private set; }
        
        public void AssignPsychologist(Psychologist psychologist)
        {
            Psychologist = psychologist;
        }
        
        public void AssignClient(Client client, long psychologistId)
        {
            ValidatePsychologist(psychologistId);
            
            Client = client;
        }

        public void UpdateAvailability(long psychologistId, DateTime from, DateTime to)
        {
            ValidatePsychologist(psychologistId);
            ValidateAvailability(from, to);
            
            From = from;
            To = to;
        }
        
        private void ValidatePsychologist(long psychologistId)
        {
            if (Psychologist is null)
            {
                throw new PsychologistNotFoundException();
            }
            
            Psychologist.Validate(psychologistId);
        }
        
        private void ValidateAvailability(DateTime from, DateTime to)
        {
            if (from >= to)
            {
                throw new InvalidAvailabilityException();
            }
        }
    }
}