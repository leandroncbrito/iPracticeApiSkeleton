using iPractice.Domain.Entities;

namespace iPractice.Unit.Tests.Builders;

public class AvailabilityBuilder
{
    private long _id { get; set; } = 1;
    private Psychologist _psychologist { get; set; } = new ();
    private Client _client { get; set; } = null;
    private DateTime _from { get; set; } = DateTime.Now;
    private DateTime _to { get; set; } = DateTime.Now.AddMinutes(30);
   
    public AvailabilityBuilder WithClient(Client client)
    {
        _client = client;
        return this;
    }
    
    public AvailabilityBuilder WithFrom(DateTime from)
    {
        _from = from;
        return this;
    }
    
    public AvailabilityBuilder WithTo(DateTime to)
    {
        _to = to;
        return this;
    }
    
    public AvailabilityBuilder WithPsychologist(Psychologist psychologist)
    {
        _psychologist = psychologist;
        return this;
    }
    
    public Availability Build()
    {
        var availability = new Availability(_id, _psychologist, _from, _to);

        if (_client is not null)
        {
            availability.AssignClient(_client);
        }
        
        return availability;
    }
}