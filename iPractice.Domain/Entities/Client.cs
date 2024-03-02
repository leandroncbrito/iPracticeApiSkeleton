using System.Collections.Generic;

namespace iPractice.Domain.Entities
{
    public class Client
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Psychologist> Psychologists { get; set; } = new ();
        
        public List<Appointment> Appointments { get; set; } = new ();
        
        public void AddAppointment(Appointment appointment)
        {
            Appointments.Add(appointment);
        }
    }
}