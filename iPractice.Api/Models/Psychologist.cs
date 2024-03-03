using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Models;

public class Psychologist
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<TimeSlot> TimeSlots { get; set; }
    public static Psychologist FromEntity(Domain.Entities.Psychologist psychologist)
    {
        return new Psychologist
        {
            Id = psychologist.Id,
            Name = psychologist.Name,
            TimeSlots = psychologist.TimeSlots.Select(timeSlot => new TimeSlot
            {
                Id = timeSlot.Id,
                From = timeSlot.From,
                To = timeSlot.To
            })
        };
    }
}