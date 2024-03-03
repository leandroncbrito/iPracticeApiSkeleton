namespace iPractice.Domain.Exceptions;

public class NotFoundException : DomainException
{
    protected NotFoundException(string message) : base(message)
    {
    }
}