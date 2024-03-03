using iPractice.Application.Commands;
using iPractice.Application.Interfaces;
using iPractice.Domain.Entities;

namespace iPractice.Application.Services;

public class TimeSlotService : ITimeSlotService
{
    public IEnumerable<TimeSlot> GenerateTimeSlotsFromAvailability(Psychologist psychologist, CreateTimeSlotsCommand createTimeSlotsCommand)
    {
        var timeSlots = new List<TimeSlot>();
        
        var date = createTimeSlotsCommand.From;
        var end = createTimeSlotsCommand.To;
        
        while (date < end)
        {
            if (date.AddMinutes(30) > end)
            {
                break;
            }

            var timeSlot = new TimeSlot(date, date.AddMinutes(30));
            
            timeSlot.AssignPsychologist(psychologist);
            
            timeSlots.Add(timeSlot);
            
            date = date.AddMinutes(30);
        }
        
        return timeSlots;
    }
}