using System.Threading.Tasks;
using iPractice.Domain.Entities;

namespace iPractice.DataAccess.Interfaces;

public interface ITimeSlotRepository
{
    Task UpdateTimeSlotAsync(TimeSlot timeSlot);
    Task<TimeSlot> GetTimeSlotAsync(long id);
}