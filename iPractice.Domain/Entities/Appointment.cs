namespace iPractice.Domain.Entities;

public class Appointment
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public long AvailabilityId { get; set; }
    //public long PsychologistId { get; set; }
}