namespace iPractice.Domain.Exceptions;

public sealed class InvalidAvailabilityException : DomainException
{
    public InvalidAvailabilityException() : base("Invalid availability provided for time slot")
    {
    }
}