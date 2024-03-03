using iPractice.Domain.Entities;

namespace iPractice.Unit.Tests.Builders;

public class ClientBuilder
{
    private long _id { get; set; } = 1;
    private string _name { get; set; } = "Client Test";
    private List<Psychologist> _psychologists { get; set; } = new ();
    
    public ClientBuilder WithId(long id)
    {
        _id = id;
        return this;
    }
    
    public ClientBuilder AddPsychologists(Psychologist psychologist)
    {
        _psychologists.Add(psychologist);
        return this;
    }
    
    public Client Build()
    {
        return new Client
        {
            Id = _id,
            Name = _name,
            Psychologists = _psychologists.ToList()
        };
    }
}