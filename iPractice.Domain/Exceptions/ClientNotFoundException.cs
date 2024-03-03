using iPractice.Domain.Entities;

namespace iPractice.Domain.Exceptions;

public sealed class ClientNotFoundException : DomainException
{
    public ClientNotFoundException() : base($"{nameof(Client)} not found")
    {
    }
}