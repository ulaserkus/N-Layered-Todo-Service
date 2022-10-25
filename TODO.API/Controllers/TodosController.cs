using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using TODO.Business.DTOs;
using TODO.Business.Services.Abstract;
using TODO.Shared.CustomControllers;
using TODO.Shared.Dtos;

namespace TODO.API.Controllers
{
    ///<summary>
    ///Endpoints for manages todos
    /// </summary>
    /// <remarks>
    /// Manage Todo records from this API
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : CustomBaseController
    {
        private readonly ITodosManager _todosManager;

        public TodosController(ITodosManager todosManager)
        {
            _todosManager = todosManager;
        }

        ///<summary>
        ///Endpoint adds a new Todo record
        /// </summary>
        /// <remarks>
        /// Title is required , duedate is optional but if you don't send default is tomorrow
        /// 
        /// Sample body request:
        ///
        ///     POST /Todos
        ///     {
        ///        "Title": "Item1",
        ///        "DueDate": "2022-10-30"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Add New Todo")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Validation errors", typeof(Error))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server exception")]
        public async Task<IActionResult> Add(TodoCreateDto todo)
        {
            if (ModelState.IsValid)
            {
                var response = await _todosManager.CreateTodoAsync(todo);
                return CreateActionResultInstance(response);
            }

            return CreateActionValidateErrors();
        }

        ///<summary>
        ///Endpoint updates a existing Todo record
        /// </summary>
        /// <remarks>
        /// Query parameter id and body elements title , IsMarked are required , duedate is optional but if you don't send default is old record's date will set
        /// 
        /// Sample body request:
        ///
        ///     PATCH /Todos
        ///     {
        ///        "Title": "Item2",
        ///        "DueDate": "2022-10-30",
        ///        "IsMarked": true
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        [HttpPatch]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Update Todo")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Wrong id usage")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Validation errors", typeof(Error))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server exception")]
        public async Task<IActionResult> Update([Required] int id, [FromBody]TodoUpdateDto todo)
        {
            if (!ModelState.IsValid) return CreateActionValidateErrors();

            var response = await _todosManager.UpdateTodoAsync(id, todo);
            return CreateActionResultInstance(response);
        }

        ///<summary>
        ///Endpoint deletes a existing Todo record
        /// </summary>
        /// <remarks>
        /// Query parameter id is required 
        /// </remarks>
        /// <param name="id"></param>
        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Delete Todo")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Wrong id usage")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server exception")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            var response = await _todosManager.DeleteTodoAsync(id);
            return CreateActionResultInstance(response);
        }

        ///<summary>
        ///Endpoint gets a existing overdue Todo records
        /// </summary>
        /// <remarks>
        /// No requires this endpoint 
        /// 
        /// 
        /// GET  /api/Todos/GetOverdues
        /// </remarks>
        [HttpGet]
        [Route("api/Todos/GetOverdues")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Overdue Todos", typeof(IEnumerable<TodoReturnDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server exception")]
        public async Task<IActionResult> GetOverdueTodos()
        {
            //if (!ModelState.IsValid) return CreateActionValidateErrors();

            var response = await _todosManager.GetOverdueTodosAsync();
            return CreateActionResultInstance(response);
        }

        ///<summary>
        ///Endpoint gets a existing pending Todo records
        /// </summary>
        /// <remarks>
        /// No requires this endpoint 
        /// 
        /// 
        /// GET  /api/Todos/GetPendings
        /// </remarks>
        [HttpGet]
        [Route("api/Todos/GetPendings")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Pendings Todos", typeof(IEnumerable<TodoReturnDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server exception")]
        public async Task<IActionResult> GetPendingTodos()
        {
            //if (!ModelState.IsValid) return CreateActionValidateErrors();

            var response = await _todosManager.GetPendingTodosAsync();
            return CreateActionResultInstance(response);
        }

    }
}
