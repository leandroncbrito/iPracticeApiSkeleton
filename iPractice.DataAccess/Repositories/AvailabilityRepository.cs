using System.Threading.Tasks;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess.Repositories;

public class AvailabilityRepository : IAvailabilityRepository
{
    private readonly IRepository<Availability> _repository;

    public AvailabilityRepository(IRepository<Availability> repository)
    {
        _repository = repository;
    }

    public async Task UpdateAvailabilityAsync(Availability client)
    {
        await _repository.UpdateAsync(client);
    }

    public async Task<Availability> GetAvailabilityAsync(long id)
    {
        return await _repository.GetAsync(f => f.Id == id, i => i
            .Include(p => p.Psychologist)
            .Include(c => c.Client));
    }
}