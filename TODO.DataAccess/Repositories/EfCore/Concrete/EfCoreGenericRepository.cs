using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TODO.DataAccess.Repositories.EfCore.Abstract;

namespace TODO.DataAccess.Repositories.EfCore.Concrete
{
    public class EfCoreGenericRepository<TContext, TEntity> : IEfCoreGenericRepository<TEntity>
        where TContext : DbContext, new()
        where TEntity : class, new()
    {

        public async Task<bool> AddAsync(TEntity entity)
        {
            try
            {
                using (TContext DbContext = new TContext())
                {
                    await DbContext.AddAsync(entity).ConfigureAwait(false);
                    await DbContext.SaveChangesAsync();
                    return (true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                using (TContext DbContext = new TContext())
                {
                    DbContext.Remove(entity);
                    await DbContext.SaveChangesAsync();
                    return (true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                using (TContext DbContext = new TContext())
                {
                    return await DbContext.Set<TEntity>().ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> query)
        {
            try
            {
                using (TContext DbContext = new TContext())
                {
                    return await DbContext.Set<TEntity>().Where(query).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                using (TContext DbContext = new TContext())
                {
                    DbContext.Update(entity);
                    await DbContext.SaveChangesAsync();
                    return (true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
