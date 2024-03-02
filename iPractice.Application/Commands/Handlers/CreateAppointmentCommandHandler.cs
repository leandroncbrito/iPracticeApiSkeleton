using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;

namespace iPractice.Application.Commands.Handlers;

public class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand>
{
    private readonly IClientRepository _clientRepository;
    
    public CreateAppointmentCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task HandleAsync(CreateAppointmentCommand createAppointmentCommand)
    {
        var client = await _clientRepository.GetClientAsync(createAppointmentCommand.ClientId);
        
        if (client is null)
        {
            throw new NullReferenceException("Client not found");
        }
        
        await _clientRepository.UpdateClientAsync(client);
    }
}