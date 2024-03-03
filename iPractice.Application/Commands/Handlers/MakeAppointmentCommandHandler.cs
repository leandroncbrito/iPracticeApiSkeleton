using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;

namespace iPractice.Application.Commands.Handlers;

public class MakeAppointmentCommandHandler : ICommandHandler<MakeAppointmentCommand>
{
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly IClientRepository _clientRepository;
    
    public MakeAppointmentCommandHandler(IAvailabilityRepository availabilityRepository, IClientRepository clientRepository)
    {
        _availabilityRepository = availabilityRepository;
        _clientRepository = clientRepository;
    }
    
    public async Task HandleAsync(MakeAppointmentCommand makeAppointmentCommand)
    {
        var client = await GetClientAsync(makeAppointmentCommand);
        
        var timeSlot = await GetTimeSlotAsync(makeAppointmentCommand);

        timeSlot.AssignClient(client);
        
        await _availabilityRepository.UpdateAvailabilityAsync(timeSlot);
    }

    private async Task<Availability> GetTimeSlotAsync(MakeAppointmentCommand makeAppointmentCommand)
    {
        var timeSlot = await _availabilityRepository.GetAvailabilityAsync(makeAppointmentCommand.TimeSlotId);
        
        if (timeSlot is null)
        {
            throw new TimeSlotNotFoundException();
        }
        
        return timeSlot;
    }
    
    private async Task<Client> GetClientAsync(MakeAppointmentCommand makeAppointmentCommand)
    {
        var client = await _clientRepository.GetClientAsync(makeAppointmentCommand.ClientId);
        
        if (client is null)
        {
            throw new ClientNotFoundException();
        }
        
        return client;
    }
}