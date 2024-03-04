using iPractice.Domain.Entities;

namespace iPractice.Unit.Tests.Builders;

public class ClientBuilder
{
    private Client _client { get; set; }
    private long _id { get; set; } = 1;
    private string _name { get; set; } = "Client Test";

    public ClientBuilder()
    {
        _client = new Client
        {
            Id = _id,
            Name = _name
        };
    }
    
    public ClientBuilder AddPsychologists(Psychologist psychologist)
    {
        _client.Psychologists.Add(psychologist);
        return this;
    }
    
    public Client Build()
    {
        return _client;
    }
}