using iPractice.Domain.Entities;

namespace iPractice.Unit.Tests.Builders;

public class PsychologistBuilder
{
    private long _id { get; set; } = 1;
    private string _name { get; set; } = "Psychologist Test";
    
    private List<Availability> _availabilities { get; set; } = new ();
    
    public PsychologistBuilder WithId(long id)
    {
        _id = id;
        return this;
    }
    
    public PsychologistBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    // public PsychologistBuilder AddAvailability(Availability availability)
    // {
    //     _availabilities.Add(availability);
    //     return this;
    // }
    
    public List<Psychologist> BuildMany(int availabilityQuantity = 2)
    {
        var psychologists = new List<Psychologist>();
        
        for (var i = 1; i <= 2; i++)
        {
            var psychologist = new PsychologistBuilder()
                .WithId(i)
                .WithName(_name + $" {i}")
                .Build();
            
            var availabilities = new List<Availability>();

            for (var j = 1; j <= availabilityQuantity; j++)
            {
                availabilities.Add(new AvailabilityBuilder()
                    .WithId(int.Parse($"{i}{j}")) 
                    .WithFrom(DateTime.Now.AddHours(j))
                    .WithTo(DateTime.Now.AddHours(j).AddMinutes(30))
                    .WithPsychologist(psychologist)
                    .Build());
            }
            
            psychologist.AddAvailability(availabilities);
            psychologists.Add(psychologist);
        }

        return psychologists;
    }
    
    public Psychologist Build()
    {
        return new Psychologist
        {
            Id = _id,
            Name = _name,
            Availabilities = _availabilities.ToList()
        };
    }
}