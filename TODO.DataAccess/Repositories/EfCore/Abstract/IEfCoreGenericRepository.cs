using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TODO.DataAccess.Repositories.EfCore.Abstract
{
    public interface IEfCoreGenericRepository<TEntity>
    {
        Task<bool> AddAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> query);
    }

}
