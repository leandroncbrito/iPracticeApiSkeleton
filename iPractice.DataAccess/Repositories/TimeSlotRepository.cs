using System.Threading.Tasks;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess.Repositories;

public class TimeSlotRepository : ITimeSlotRepository
{
    private readonly IRepository<TimeSlot> _repository;

    public TimeSlotRepository(IRepository<TimeSlot> repository)
    {
        _repository = repository;
    }

    public async Task UpdateTimeSlotAsync(TimeSlot client)
    {
        await _repository.UpdateAsync(client);
    }

    public async Task<TimeSlot> GetTimeSlotAsync(long id)
    {
        return await _repository.GetAsync(f => f.Id == id, i => i
            .Include(p => p.Psychologist)
            .Include(c => c.Client));
    }
}