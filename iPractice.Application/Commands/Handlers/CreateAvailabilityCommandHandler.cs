using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;

namespace iPractice.Application.Commands.Handlers;

public class CreateAvailabilityCommandHandler : ICommandHandler<CreateAvailabilityCommand>
{
    private readonly IPsychologistRepository _psychologistRepository;

    public CreateAvailabilityCommandHandler(IPsychologistRepository psychologistRepository)
    {
        _psychologistRepository = psychologistRepository;
    }

    public async Task HandleAsync(CreateAvailabilityCommand createAvailabilityCommand)
    {
        //TODO: add service to handle availability in batches

        var psychologist = await _psychologistRepository.GetPsychologistAsync(createAvailabilityCommand.PsychologistId);

        if (psychologist is null)
        {
            throw new NullReferenceException("Psychologist not found");
        }

        //TODO: validate availability
        var availability = new Availability
        {
            From = createAvailabilityCommand.From,
            To = createAvailabilityCommand.To,
            Psychologist = psychologist
        };
        
        psychologist.AddAvailability(availability);

        await _psychologistRepository.UpdatePsychologistAsync(psychologist);
    }
}