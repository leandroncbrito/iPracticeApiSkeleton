using iPractice.Domain.Entities;

namespace iPractice.Domain.Exceptions;

public sealed class PsychologistMismatchException : DomainException
{
    public PsychologistMismatchException(string message) : base(message)
    {
    }
    
    public PsychologistMismatchException() : base($"{nameof(Psychologist)} mismatch")
    {
    }
}