using System.Threading.Tasks;
using iPractice.Domain.Entities;

namespace iPractice.DataAccess.Interfaces;

public interface IAvailabilityRepository
{
    Task InsertAsync(Availability timeSlot);
}