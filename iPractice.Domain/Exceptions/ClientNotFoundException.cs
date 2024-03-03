using iPractice.Domain.Entities;

namespace iPractice.Domain.Exceptions;

public sealed class ClientNotFoundException : NotFoundException
{
    public ClientNotFoundException() : base($"{nameof(Client)} not found")
    {
    }
}