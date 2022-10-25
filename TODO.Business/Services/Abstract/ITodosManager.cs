using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Business.DTOs;
using TODO.DataAccess.Models;
using TODO.Shared.Dtos;

namespace TODO.Business.Services.Abstract
{
    public interface ITodosManager
    {
        Task<Response<NoContent>> CreateTodoAsync(TodoCreateDto todo);

        Task<Response<NoContent>> UpdateTodoAsync(int id, TodoUpdateDto todo);

        Task<Response<NoContent>> DeleteTodoAsync(int id);

        Task<Response<IEnumerable<TodoReturnDto>>> GetOverdueTodosAsync();

        Task<Response<IEnumerable<TodoReturnDto>>> GetPendingTodosAsync();
    }
}
