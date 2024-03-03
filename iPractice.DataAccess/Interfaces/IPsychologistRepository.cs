using System.Threading.Tasks;
using iPractice.Domain.Entities;

namespace iPractice.DataAccess.Interfaces;

public interface IPsychologistRepository
{
    Task UpdatePsychologistAsync(Psychologist psychologist);
    Task<Psychologist> GetPsychologistAsync(long id);
}