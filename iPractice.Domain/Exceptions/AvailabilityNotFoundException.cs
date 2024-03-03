using iPractice.Domain.Entities;

namespace iPractice.Domain.Exceptions;

public sealed class AvailabilityNotFoundException : NotFoundException
{
    public AvailabilityNotFoundException() : base($"{nameof(Availability)} not found")
    {
    }
}