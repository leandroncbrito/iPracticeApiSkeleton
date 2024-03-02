using System.Threading.Tasks;
using iPractice.Domain.Entities;

namespace iPractice.DataAccess.Interfaces;

public interface IPsychologistRepository
{
    Task<Psychologist?> GetAsync(long id);
}