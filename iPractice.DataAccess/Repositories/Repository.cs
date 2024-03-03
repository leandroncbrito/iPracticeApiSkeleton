using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iPractice.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace iPractice.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _dbSet.Update(entity);
        await SaveChangesAsync();
    }

    public async Task<TEntity> GetAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true)
    {
        var query = BuildQuery(filter, include, disableTracking);

        return await query.FirstOrDefaultAsync();
    }

    private IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> filter, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, 
        bool disableTracking = true)
    {
        if (disableTracking)
        {
            _dbSet.AsNoTrackingWithIdentityResolution();
        }

        IQueryable<TEntity> query = _dbSet;
        
        if (include != null)
        {
            query = include(query);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        return query;
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}