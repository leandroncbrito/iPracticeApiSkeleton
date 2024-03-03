using iPractice.Domain.Entities;

namespace iPractice.Domain.Exceptions;

public sealed class PsychologistMismatchException : DomainException
{
    public PsychologistMismatchException() : base($"{nameof(Psychologist)} mismatch")
    {
    }
}