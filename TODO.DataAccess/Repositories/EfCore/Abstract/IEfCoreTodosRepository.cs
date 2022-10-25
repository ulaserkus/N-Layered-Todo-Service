using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.DataAccess.Models;

namespace TODO.DataAccess.Repositories.EfCore.Abstract
{
    public interface IEfCoreTodosRepository : IEfCoreGenericRepository<Todo>
    {
        ValueTask<Todo> GetByIdAsync(int id);
    }
}
