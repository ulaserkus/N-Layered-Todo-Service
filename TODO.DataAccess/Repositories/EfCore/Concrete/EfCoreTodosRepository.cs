using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.DataAccess.Models;
using TODO.DataAccess.Repositories.EfCore.Abstract;
using TODO.DataAccess.Repositories.EfCore.Context;

namespace TODO.DataAccess.Repositories.EfCore.Concrete
{
    public class EfCoreTodosRepository : EfCoreGenericRepository<EfCoreDbContext, Todo>, IEfCoreTodosRepository
    {
        public ValueTask<Todo> GetByIdAsync(int id)
        {
            try
            {
                using (EfCoreDbContext DbContext = new EfCoreDbContext())
                {
                    return DbContext.Set<Todo>().FindAsync(id);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
