using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace iPractice.DataAccess.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task UpdateAsync(TEntity entity);
    
    Task<TEntity> GetAsync(long id);

    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = false);
}