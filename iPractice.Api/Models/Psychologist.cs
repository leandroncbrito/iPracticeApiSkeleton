using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Models;

public class Psychologist
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Availability> Availabilities { get; set; }
    public static Psychologist FromEntity(Domain.Entities.Psychologist psychologist)
    {
        return new Psychologist
        {
            Id = psychologist.Id,
            Name = psychologist.Name,
            Availabilities = psychologist.Availabilities.Select(a => new Availability
            {
                Id = a.Id,
                From = a.From,
                To = a.To
            })
        };
    }
}