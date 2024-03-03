namespace iPractice.Application.Commands;

public class UpdateAvailabilityCommand
{
    public long PsychologistId { get; private set; }
    
    public long TimeSlotId { get; private set; }
    
    public DateTime From { get; private set; }
    
    public DateTime To { get; private set; }
    
    public UpdateAvailabilityCommand(long psychologistId, long timeSlotId, DateTime from, DateTime to)
    {
        PsychologistId = psychologistId;
        TimeSlotId = timeSlotId;
        From = from;
        To = to;
    }
}