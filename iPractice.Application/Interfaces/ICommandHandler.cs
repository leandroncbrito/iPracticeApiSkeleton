namespace iPractice.Application.Interfaces;

public interface ICommandHandler<in TCommand>
{
    Task HandleAsync(TCommand command);
}