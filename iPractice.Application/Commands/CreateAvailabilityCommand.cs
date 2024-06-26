﻿using iPractice.Application.Interfaces;

namespace iPractice.Application.Commands;

public class CreateAvailabilityCommand : ICommand
{
    public long PsychologistId { get; private set; }
    
    public DateTime From { get; private set; }
    
    public DateTime To { get; private set; }
    
    public CreateAvailabilityCommand(long psychologistId, DateTime from, DateTime to)
    {
        PsychologistId = psychologistId;
        From = from;
        To = to;
    }    
}