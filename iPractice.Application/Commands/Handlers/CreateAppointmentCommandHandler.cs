using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;

namespace iPractice.Application.Commands.Handlers;

public class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand>
{
    private readonly ITimeSlotRepository _timeSlotRepository;
    private readonly IClientRepository _clientRepository;
    
    public CreateAppointmentCommandHandler(ITimeSlotRepository timeSlotRepository, IClientRepository clientRepository)
    {
        _timeSlotRepository = timeSlotRepository;
        _clientRepository = clientRepository;
    }
    
    public async Task HandleAsync(CreateAppointmentCommand createAppointmentCommand)
    {
        var timeSlot = await GetTimeSlotAsync(createAppointmentCommand);
        
        var client = await GetClientAsync(createAppointmentCommand);

        timeSlot.AssignClient(client, createAppointmentCommand.PsychologistId);
        
        await _timeSlotRepository.UpdateTimeSlotAsync(timeSlot);
    }

    private async Task<TimeSlot> GetTimeSlotAsync(CreateAppointmentCommand createAppointmentCommand)
    {
        var timeSlot = await _timeSlotRepository.GetTimeSlotAsync(createAppointmentCommand.TimeSlotId);
        
        if (timeSlot is null)
        {
            throw new TimeSlotNotFoundException();
        }
        
        return timeSlot;
    }
    
    private async Task<Client> GetClientAsync(CreateAppointmentCommand createAppointmentCommand)
    {
        var client = await _clientRepository.GetClientAsync(createAppointmentCommand.ClientId);
        
        if (client is null)
        {
            throw new ClientNotFoundException();
        }
        
        return client;
    }
}