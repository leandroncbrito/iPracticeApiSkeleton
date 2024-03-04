using iPractice.Domain.Entities;

namespace iPractice.Unit.Tests.Builders;

public class PsychologistBuilder
{
    private Psychologist _psychologist { get; set; }
    private long _id { get; set; } = 1;
    private string _name { get; set; } = "Psychologist Test";
    private List<Availability> _availabilities { get; set; } = new ();

    public PsychologistBuilder()
    {
        _psychologist = new Psychologist
        {
            Id = _id,
            Name = _name
        };
    }
    
    public PsychologistBuilder WithId(long id)
    {
        _psychologist.Id = id;
        return this;
    }
    
    public PsychologistBuilder AddAvailability()
    {
        var availability= new AvailabilityBuilder()
            .WithPsychologist(_psychologist)
            .Build();
        
        _availabilities.Add(availability);

        return this;
    }
    
    public Psychologist Build()
    {
        _psychologist.AddAvailability(_availabilities);
        
        return _psychologist;
    }
}