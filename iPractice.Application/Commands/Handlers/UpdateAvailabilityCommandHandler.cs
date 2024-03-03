using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;

namespace iPractice.Application.Commands.Handlers;

public class UpdateAvailabilityCommandHandler : ICommandHandler<UpdateAvailabilityCommand>
{
    private readonly ITimeSlotRepository _timeSlotRepository;

    public UpdateAvailabilityCommandHandler(ITimeSlotRepository timeSlotRepository)
    {
        _timeSlotRepository = timeSlotRepository;
    }

    public async Task HandleAsync(UpdateAvailabilityCommand updateAvailabilityCommand)
    {
        var timeSlot = await GetTimeSlotAsync(updateAvailabilityCommand);
        
        timeSlot.UpdateAvailability(updateAvailabilityCommand.PsychologistId, updateAvailabilityCommand.From, updateAvailabilityCommand.To);

        await _timeSlotRepository.UpdateTimeSlotAsync(timeSlot);
    }
    
    private async Task<TimeSlot> GetTimeSlotAsync(UpdateAvailabilityCommand updateAvailabilityCommand)
    {
        var timeSlot = await _timeSlotRepository.GetTimeSlotAsync(updateAvailabilityCommand.TimeSlotId);
        
        if (timeSlot is null)
        {
            throw new TimeSlotNotFoundException();
        }
        
        return timeSlot;
    }
}