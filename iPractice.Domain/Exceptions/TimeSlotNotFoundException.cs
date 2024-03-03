using iPractice.Domain.Entities;

namespace iPractice.Domain.Exceptions;

public sealed class TimeSlotNotFoundException : DomainException
{
    public TimeSlotNotFoundException() : base($"{nameof(TimeSlot)} not found")
    {
    }
}