using iPractice.Application.Interfaces;

namespace iPractice.Application.Commands;

public class CreateAppointmentCommand : ICommand
{
    public long ClientId { get; private set; }

    public long TimeSlotId { get; private set; }

    public CreateAppointmentCommand(long clientId, long timeSlotId)
    {
        ClientId = clientId;
        TimeSlotId = timeSlotId;
    }
}