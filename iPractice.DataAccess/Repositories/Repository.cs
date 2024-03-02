using System;
using System.Threading.Tasks;
using iPractice.DataAccess.Interfaces;

namespace iPractice.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    
    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task InsertAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _context.AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task<TEntity?> GetAsync(long id)
    {
        return await _context.FindAsync<TEntity>(id);
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    // private bool _disposed;

    // protected virtual void Dispose(bool disposing)
    // {
    //     if (!_disposed)
    //     {
    //         if (disposing)
    //         {
    //             _context.Dispose();
    //         }
    //     }
    //     
    //     _disposed = true;
    // }
    //
    // public void Dispose()
    // {
    //     Dispose(true);
    //     GC.SuppressFinalize(this);
    // }
}