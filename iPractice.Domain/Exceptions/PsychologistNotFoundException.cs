using iPractice.Domain.Entities;

namespace iPractice.Domain.Exceptions;

public sealed class PsychologistNotFoundException : DomainException
{
    public PsychologistNotFoundException() : base($"{nameof(Psychologist)} not found")
    {
    }
}