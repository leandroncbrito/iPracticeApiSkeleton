namespace iPractice.Application.Interfaces;

public interface IQueryHandler<in TQuery, TResult> where TQuery : class
{
    Task<TResult> HandleAsync(TQuery command);
}