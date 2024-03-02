using System;
using System.Threading.Tasks;

namespace iPractice.DataAccess.Interfaces;

// public interface IRepository<TEntity> : IDisposable where TEntity : class
public interface IRepository<TEntity> where TEntity : class
{
    Task InsertAsync(TEntity entity);
    
    Task<TEntity?> GetAsync(long id);
    // Task<int> SaveChangesAsync();
}