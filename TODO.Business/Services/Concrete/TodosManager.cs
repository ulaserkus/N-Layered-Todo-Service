using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TODO.Business.DTOs;
using TODO.Business.Services.Abstract;
using TODO.Business.Utils.Maps;
using TODO.DataAccess.Models;
using TODO.DataAccess.Repositories.EfCore.Abstract;
using TODO.Shared.Dtos;
using System.Linq;

namespace TODO.Business.Services.Concrete
{
    public class TodosManager : ITodosManager
    {
        private readonly IEfCoreTodosRepository _todosRepository;

        public TodosManager(IEfCoreTodosRepository todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<Response<NoContent>> CreateTodoAsync(TodoCreateDto todo)
        {
            try
            {
                if (todo.DueDate is null) todo.DueDate = DateTime.Now.AddDays(1);

                var newTodo = Mapping.Mapper.Map<Todo>(todo);
                newTodo.IsMarked = false;

                bool status = await _todosRepository.AddAsync(newTodo);

                if (status) return Response<NoContent>.Success(201);
                else return Response<NoContent>.Fail("Record not created", 400);
            }
            catch (Exception ex)
            {
                return Response<NoContent>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<NoContent>> DeleteTodoAsync(int id)
        {
            try
            {
                Todo findingTodo = await _todosRepository.GetByIdAsync(id);

                if (!(findingTodo is null))
                {
                    bool status = await _todosRepository.DeleteAsync(findingTodo);

                    if (status) return Response<NoContent>.Success(204);
                }
                else
                {
                    return Response<NoContent>.Fail("Todo is not found", 404);
                }

                return Response<NoContent>.Fail("Unexpected error occured", 400);
            }
            catch (Exception ex)
            {
                return Response<NoContent>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<IEnumerable<TodoReturnDto>>> GetAllTodosAsync()
        {
            try
            {
                var todos = await _todosRepository.GetAllAsync();
                var todosDto = Mapping.Mapper.Map<List<TodoReturnDto>>(todos);
                return Response<IEnumerable<TodoReturnDto>>.Success(todosDto, 200);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<TodoReturnDto>>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<IEnumerable<TodoReturnDto>>> GetPendingTodosAsync()
        {
            try
            {
                var todos = await _todosRepository.GetAllAsync();
                var pendingTodos = todos.AsQueryable().Where(x => x.DueDate >= DateTime.Now.Date && x.IsMarked == false).ToList();
                var returnDtos = Mapping.Mapper.Map<IEnumerable<TodoReturnDto>>(pendingTodos);
                return Response<IEnumerable<TodoReturnDto>>.Success(returnDtos, 200);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<TodoReturnDto>>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<IEnumerable<TodoReturnDto>>> GetOverdueTodosAsync()
        {
            try
            {
                var todos = await _todosRepository.GetAllAsync();
                var overdueTodos = todos.AsQueryable().Where(x => x.DueDate < DateTime.Now.Date && x.IsMarked == false).ToList();
                var returnDtos = Mapping.Mapper.Map<IEnumerable<TodoReturnDto>>(overdueTodos);
                return Response<IEnumerable<TodoReturnDto>>.Success(returnDtos, 200);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<TodoReturnDto>>.Fail(ex.Message, 500);
            }
        }


        public async Task<Response<NoContent>> UpdateTodoAsync(int id, TodoUpdateDto todo)
        {
            try
            {
                if (id.Equals(null)) return Response<NoContent>.Fail("Todo is not found", 404);

                Todo oldTodo = await _todosRepository.GetByIdAsync(id);

                if (todo.DueDate is null && oldTodo.DueDate is null) todo.DueDate = DateTime.Now.AddDays(1);
                else if (todo.DueDate is null && !(oldTodo is null)) todo.DueDate = oldTodo.DueDate;

                if (!(oldTodo is null))
                {
                    Todo newTodo = Mapping.Mapper.Map<Todo>(todo);

                    newTodo.TodoId = oldTodo.TodoId;

                    bool status = await _todosRepository.UpdateAsync(newTodo);

                    if (status) return Response<NoContent>.Success(204);
                }
                else
                {
                    return Response<NoContent>.Fail("Todo is not found", 404);
                }

                return Response<NoContent>.Fail("Unexpected error occured", 400);
            }
            catch (Exception ex)
            {
                return Response<NoContent>.Fail(ex.Message, 500);
            }
        }
    }
}
