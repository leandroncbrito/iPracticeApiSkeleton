namespace iPractice.Application.Interfaces;

public interface IQueryHandler<TQuery, TResult> where TQuery : class
{
    Task<TResult> HandleAsync(TQuery command);
}