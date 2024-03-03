using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;

namespace iPractice.Application.Commands.Handlers;

public class UpdateAvailabilityCommandHandler : ICommandHandler<UpdateAvailabilityCommand>
{
    private readonly IAvailabilityRepository _availabilityRepository;

    public UpdateAvailabilityCommandHandler(IAvailabilityRepository availabilityRepository)
    {
        _availabilityRepository = availabilityRepository;
    }

    public async Task HandleAsync(UpdateAvailabilityCommand updateAvailabilityCommand)
    {
        var availability = await GetAvailabilityAsync(updateAvailabilityCommand);
        
        availability.UpdateAvailability(updateAvailabilityCommand.PsychologistId, updateAvailabilityCommand.From, updateAvailabilityCommand.To);

        await _availabilityRepository.UpdateAvailabilityAsync(availability);
    }
    
    private async Task<Availability> GetAvailabilityAsync(UpdateAvailabilityCommand updateAvailabilityCommand)
    {
        var availability = await _availabilityRepository.GetAvailabilityAsync(updateAvailabilityCommand.AvailabilityId);
        
        if (availability is null)
        {
            throw new AvailabilityNotFoundException();
        }
        
        return availability;
    }
}