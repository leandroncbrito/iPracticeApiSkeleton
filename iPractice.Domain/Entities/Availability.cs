using iPractice.Domain.Exceptions;

namespace iPractice.Domain.Entities
{
    public class Availability
    {
        protected Availability()
        {
        }
        
        public Availability(Psychologist psychologist, DateTime from, DateTime to )
        {
            Psychologist = psychologist;
            From = from;
            To = to;
        }
        
        public long Id { get; private set; }
        
        public DateTime From { get; private set; }
        
        public DateTime To { get; private set; }

        public Psychologist Psychologist { get; private set; }
        
        public Client Client { get; private set; }
        
        public void AssignClient(Client client)
        {
            Validate(client);
            
            Client = client;
        }

        public void UpdateAvailability(long psychologistId, DateTime from, DateTime to)
        {
            Psychologist.Validate(psychologistId);
            
            From = from;
            To = to;
        }
        
        private void Validate(Client client)
        {
            if (Client is not null)
            {
                throw new ClientAlreadyAssignedException();
            }
            
            if (Psychologist is null)
            {
                throw new PsychologistNotFoundException();
            }
            
            if (client.Psychologists.Contains(Psychologist) is false)
            {
                throw new PsychologistMismatchException($"{nameof(Psychologist)} is not available for this client.");
            }
        }
    }
}