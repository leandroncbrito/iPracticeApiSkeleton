using System.Threading.Tasks;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess.Repositories;

public class PsychologistRepository : IPsychologistRepository
{
    private readonly IRepository<Psychologist> _repository;
    
    public PsychologistRepository(IRepository<Psychologist> repository)
    {
        _repository = repository;
    }

    public async Task UpdatePsychologistAsync(Psychologist psychologist)
    {
        await _repository.UpdateAsync(psychologist);
    }

    public async Task<Psychologist> GetPsychologistAsync(long id)
    {
        return await _repository.GetAsync(f => f.Id == id, i => i
            .Include(p => p.TimeSlots));
    }
}