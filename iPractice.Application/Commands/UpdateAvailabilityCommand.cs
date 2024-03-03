namespace iPractice.Application.Commands;

public class UpdateAvailabilityCommand
{
    public long PsychologistId { get; private set; }
    
    public long AvailabilityId { get; private set; }
    
    public DateTime From { get; private set; }
    
    public DateTime To { get; private set; }
    
    public UpdateAvailabilityCommand(long psychologistId, long availabilityId, DateTime from, DateTime to)
    {
        PsychologistId = psychologistId;
        AvailabilityId = availabilityId;
        From = from;
        To = to;
    }
}