namespace iPractice.Application.Interfaces;

public interface ICommandHandler<TCommand>
{
    Task HandleAsync(TCommand command);
}