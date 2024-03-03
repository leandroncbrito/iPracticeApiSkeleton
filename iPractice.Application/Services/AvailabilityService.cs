using iPractice.Application.Commands;
using iPractice.Application.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;

namespace iPractice.Application.Services;

public class AvailabilityService : IAvailabilityService
{
    private const int TimeSlotDuration = 30;
    
    public IEnumerable<Availability> GenerateAvailabilitiesFromBatch(Psychologist psychologist, CreateAvailabilityCommand createAvailabilityCommand)
    {
        ValidateAvailability(createAvailabilityCommand.From, createAvailabilityCommand.To);
        
        var timeSlots = new List<Availability>();
        
        var date = createAvailabilityCommand.From;
        var end = createAvailabilityCommand.To;
        
        while (date < end)
        {
            if (date.AddMinutes(TimeSlotDuration) > end)
            {
                break;
            }

            var timeSlot = new Availability(psychologist, date, date.AddMinutes(TimeSlotDuration));
            
            timeSlots.Add(timeSlot);
            
            date = date.AddMinutes(TimeSlotDuration);
        }
        
        return timeSlots;
    }

    private void ValidateAvailability(DateTime from, DateTime to)
    {
        if (from < DateTime.Now || to < DateTime.Now)
        {
            throw new InvalidAvailabilityException("Availability cannot be in the past");
        }
        
        if (from > to)
        {
            throw new InvalidAvailabilityException("Availability cannot end before it starts");
        }
    }
}