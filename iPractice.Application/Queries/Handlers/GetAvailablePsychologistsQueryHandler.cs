using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;

namespace iPractice.Application.Queries.Handlers;

public class
    GetAvailablePsychologistsQueryHandler : IQueryHandler<GetAvailablePsychologistsQuery, IEnumerable<Psychologist>>
{
    private readonly IClientRepository _clientRepository;

    public GetAvailablePsychologistsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Psychologist>> HandleAsync(
        GetAvailablePsychologistsQuery getAvailablePsychologistsQuery)
    {
        var client = await _clientRepository.GetClientAsync(getAvailablePsychologistsQuery.ClientId);

        if (client is null)
        {
            throw new NullReferenceException("Client not found");
        }

        return client.Psychologists;
    }
}