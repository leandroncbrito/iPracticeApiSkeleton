using iPractice.Application.Interfaces;
using iPractice.Domain.Entities;

namespace iPractice.Application.Queries;

public class GetAvailablePsychologistsQuery : IQuery<IEnumerable<Psychologist>>
{
    public long ClientId { get; private set; }
    
    public GetAvailablePsychologistsQuery(long clientId)
    {
        ClientId = clientId;
    }
}