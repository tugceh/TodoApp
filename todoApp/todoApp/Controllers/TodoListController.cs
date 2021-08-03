using System;
using System.Threading.Tasks;
using Info;
using Microsoft.AspNetCore.Mvc;
using todoApp.Data.Dtos.todolist;
using todoApp.Data.Models;
using todoApp.ServiceLayer;

namespace todoApp.Controllers
{

    [Route("api/todolist")]
    [ApiController]
    public class TodoListController : BaseApiController<TodoListController, TodoList, ITodolistService>
    {
        public TodoListController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        [HttpPost("addTodolist")]
        public async Task<IActionResult> Register([FromBody] AddTodoListRequest addTodoListRequest)
        {
            return Ok(await Service.AddTodo(addTodoListRequest));
        }

        [HttpGet("listTodos/{userid}")]
        public async Task<IActionResult> GetListTodos(Guid userid)
        {
            return Ok(await Service.GetAllTodosByUser(userid));
        }

        [HttpPost("listAllTodos")]
        public async Task<IActionResult> GetAllTodos()
        {
            return Ok(await Service.GetAllTodos());
        }

        [HttpPost("deleteTodo")]
        public async Task<IActionResult> DeleteTodo([FromBody] DeleteTodoListRequest deleteRequest)
        {
            return Ok(await Service.DeleteTodo(deleteRequest));
        }
    }
}
