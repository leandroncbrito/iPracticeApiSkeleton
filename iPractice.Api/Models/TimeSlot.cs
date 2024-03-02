using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Models
{
    public class TimeSlot
    {
        public Psychologist Psychologist { get; set; }

        private TimeSlot(Psychologist psychologist)
        {
            Psychologist = psychologist;
        }

        //TODO: move to factory
        public static IEnumerable<TimeSlot> FromEntity(IEnumerable<Domain.Entities.Psychologist> psychologistAvailabilities)
        {
            return psychologistAvailabilities.Select(x => new TimeSlot(Psychologist.FromEntity(x)));
        }
    }
}