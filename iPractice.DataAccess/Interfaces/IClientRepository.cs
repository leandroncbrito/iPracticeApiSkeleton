using System.Threading.Tasks;
using iPractice.Domain.Entities;

namespace iPractice.DataAccess.Interfaces;

public interface IClientRepository
{
    Task UpdateClientAsync(Client client);
    Task<Client?> GetClientAsync(long id);
}