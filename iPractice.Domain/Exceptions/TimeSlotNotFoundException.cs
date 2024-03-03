namespace iPractice.Domain.Exceptions;

public sealed class TimeSlotNotFoundException : NotFoundException
{
    public TimeSlotNotFoundException() : base("Time slot not found")
    {
    }
}