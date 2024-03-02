namespace iPractice.Application.Queries;

public class GetAvailablePsychologistsQuery
{
    public GetAvailablePsychologistsQuery(long clientId)
    {
        ClientId = clientId;
    }
    
    public long ClientId { get; private set; }
}