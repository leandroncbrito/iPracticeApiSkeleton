using iPractice.Application.Commands;
using iPractice.Domain.Entities;

namespace iPractice.Application.Interfaces;

public interface IAvailabilityService
{
    IEnumerable<Availability> GenerateAvailabilitiesFromBatch(Psychologist psychologist,
        CreateAvailabilityCommand createAvailabilityCommand);
}