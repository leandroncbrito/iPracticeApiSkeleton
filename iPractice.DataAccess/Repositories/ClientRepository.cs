using System.Linq;
using System.Threading.Tasks;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly IRepository<Client> _repository;
    
    public ClientRepository(IRepository<Client> repository)
    {
        _repository = repository;
    }
    
    public async Task UpdateClientAsync(Client client)
    {
        await _repository.UpdateAsync(client);
    }
    
    public async Task<Client> GetClientAsync(long id)
    {
        return await _repository.GetAsync(f => f.Id == id, i => i
            .Include(p => p.Psychologists)
            .ThenInclude(p => p.TimeSlots.Where(t => t.Client == null)),
            true);
    }
}