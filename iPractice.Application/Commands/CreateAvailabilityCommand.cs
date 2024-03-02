using iPractice.Application.Interfaces;

namespace iPractice.Application.Commands;

public class CreateAvailabilityCommand : ICommand
{
    public long PsychologistId { get; set; }
    
    public DateTime From { get; set; }
    
    public DateTime To { get; set; }
}