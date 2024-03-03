namespace iPractice.Domain.Exceptions;

public sealed class InvalidAvailabilityException : DomainException
{
    public InvalidAvailabilityException(string message) : base(message)
    {
    }
}