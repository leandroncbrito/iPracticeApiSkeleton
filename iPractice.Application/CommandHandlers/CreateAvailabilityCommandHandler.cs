using iPractice.Application.Commands;
using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;

namespace iPractice.Application.CommandHandlers;

public class CreateAvailabilityCommandHandler : ICommandHandler<CreateAvailabilityCommand>
{
    private readonly IPsychologistRepository _psychologistRepository;
    private readonly IAvailabilityRepository _availabilityRepository;
    
    public CreateAvailabilityCommandHandler(IPsychologistRepository psychologistRepository, IAvailabilityRepository availabilityRepository)
    {
        _psychologistRepository = psychologistRepository;
        _availabilityRepository = availabilityRepository;
    }
    
    public async Task HandleAsync(CreateAvailabilityCommand createAvailabilityCommand)
    {
        try
        {
            //@TODO: add service to handle availability in batches
            
            var psychologist = await _psychologistRepository.GetAsync(createAvailabilityCommand.PsychologistId);
        
            if (psychologist is null)
            {
                throw new NullReferenceException("Psychologist not found");
            }
        
            var availability = new Availability
            {
                From = createAvailabilityCommand.From,
                To = createAvailabilityCommand.To,
                Psychologist = psychologist
            };
        
            await _availabilityRepository.InsertAsync(availability);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}