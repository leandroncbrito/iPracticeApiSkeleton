using iPractice.Application.Interfaces;

namespace iPractice.Application.Commands;

public class MakeAppointmentCommand : ICommand
{
    public long ClientId { get; private set; }
    public long TimeSlotId { get; private set; }

    public MakeAppointmentCommand(long clientId, long timeSlotId)
    {
        ClientId = clientId;
        TimeSlotId = timeSlotId;
    }
}