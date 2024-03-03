using iPractice.Application.Commands;
using iPractice.Domain.Entities;

namespace iPractice.Application.Interfaces;

public interface ITimeSlotService
{
    IEnumerable<TimeSlot> GenerateTimeSlotsFromAvailability(Psychologist psychologist,
        CreateTimeSlotsCommand createTimeSlotsCommand);
}