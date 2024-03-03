using iPractice.Application.Interfaces;
using iPractice.DataAccess.Interfaces;
using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;

namespace iPractice.Application.Commands.Handlers;

public class CreateTimeSlotsCommandHandler : ICommandHandler<CreateTimeSlotsCommand>
{
    private readonly ITimeSlotService _timeSlotService;
    private readonly IPsychologistRepository _psychologistRepository;

    public CreateTimeSlotsCommandHandler(ITimeSlotService timeSlotService, IPsychologistRepository psychologistRepository)
    {
        _timeSlotService = timeSlotService;
        _psychologistRepository = psychologistRepository;
    }

    public async Task HandleAsync(CreateTimeSlotsCommand createTimeSlotsCommand)
    {
        var psychologist = await GetPsychologist(createTimeSlotsCommand);

        var timeSlots = _timeSlotService.GenerateTimeSlotsFromAvailability(psychologist, createTimeSlotsCommand);
        
        psychologist.AddTimeSlots(timeSlots);

        await _psychologistRepository.UpdatePsychologistAsync(psychologist);
    }

    private async Task<Psychologist> GetPsychologist(CreateTimeSlotsCommand createTimeSlotsCommand)
    {
        var psychologist = await _psychologistRepository.GetPsychologistAsync(createTimeSlotsCommand.PsychologistId);

        if (psychologist is null)
        {
            throw new PsychologistNotFoundException();
        }

        return psychologist;
    }
}