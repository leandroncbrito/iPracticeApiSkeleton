using System.Threading.Tasks;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;

namespace iPractice.DataAccess.Repositories;

public class PsychologistRepository : IPsychologistRepository
{
    private readonly IRepository<Psychologist> _repository;
    
    public PsychologistRepository(IRepository<Psychologist> repository)
    {
        _repository = repository;
    }   
    
    public async Task<Psychologist?> GetAsync(long id)
    {
        return await _repository.GetAsync(id);
    }
}