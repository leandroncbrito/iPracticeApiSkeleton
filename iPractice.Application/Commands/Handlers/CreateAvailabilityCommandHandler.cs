using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;

namespace iPractice.Application.Commands.Handlers;

public class CreateAvailabilityCommandHandler : ICommandHandler<CreateAvailabilityCommand>
{
    private readonly IAvailabilityService _availabilityService;
    private readonly IPsychologistRepository _psychologistRepository;

    public CreateAvailabilityCommandHandler(IAvailabilityService availabilityService, IPsychologistRepository psychologistRepository)
    {
        _availabilityService = availabilityService;
        _psychologistRepository = psychologistRepository;
    }

    public async Task HandleAsync(CreateAvailabilityCommand createAvailabilityCommand)
    {
        var psychologist = await GetPsychologist(createAvailabilityCommand);

        var timeSlots = _availabilityService.GenerateAvailabilitiesFromBatch(psychologist, createAvailabilityCommand);
        
        psychologist.AddAvailability(timeSlots);

        await _psychologistRepository.UpdatePsychologistAsync(psychologist);
    }

    private async Task<Psychologist> GetPsychologist(CreateAvailabilityCommand createAvailabilityCommand)
    {
        var psychologist = await _psychologistRepository.GetPsychologistAsync(createAvailabilityCommand.PsychologistId);

        if (psychologist is null)
        {
            throw new PsychologistNotFoundException();
        }

        return psychologist;
    }
}