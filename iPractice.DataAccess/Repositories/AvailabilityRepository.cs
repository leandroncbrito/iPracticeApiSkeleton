using System.Threading.Tasks;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;

namespace iPractice.DataAccess.Repositories;

public class AvailabilityRepository : IAvailabilityRepository
{
    private readonly IRepository<Availability> _repository;
    
    public AvailabilityRepository(IRepository<Availability> repository)
    {
        _repository = repository;
    }   
    
    public async Task InsertAsync(Availability availability)
    {
        await _repository.InsertAsync(availability);
    }
}