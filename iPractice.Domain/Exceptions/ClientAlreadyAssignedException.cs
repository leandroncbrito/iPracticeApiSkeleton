using iPractice.Domain.Entities;

namespace iPractice.Domain.Exceptions;

public sealed class ClientAlreadyAssignedException : DomainException
{
    public ClientAlreadyAssignedException() : base($"{nameof(Client)} is already assigned to this time slot")
    {
    }
}