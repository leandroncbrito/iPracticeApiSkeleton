using iPractice.Application.Interfaces;
using iPractice.Domain.Entities;

namespace iPractice.Application.Queries;

public class GetAvailablePsychologistsQuery : IQuery<IEnumerable<Psychologist>>
{
    public GetAvailablePsychologistsQuery(long clientId)
    {
        ClientId = clientId;
    }
    
    public long ClientId { get; private set; }
}