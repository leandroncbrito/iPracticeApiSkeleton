using iPractice.Domain.Entities;

namespace iPractice.Domain.Exceptions;

public sealed class PsychologistNotFoundException : NotFoundException
{
    public PsychologistNotFoundException() : base($"{nameof(Psychologist)} not found")
    {
    }
}