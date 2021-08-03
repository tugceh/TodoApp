using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Info.Initializations;
using todoApp.Data.Dtos.todolist;
using todoApp.Data.Models;

namespace todoApp.ServiceLayer
{

    public interface ITodolistService : IService<TodoList>
    {
        Task<List<TodoList>> GetAllTodosByUser(Guid userid);
        Task<AddTodoListResponse> AddTodo(AddTodoListRequest addRequest);
        Task<List<TodoList>> GetAllTodos();
        Task<DeleteTodoListResponse> DeleteTodo(DeleteTodoListRequest request);
    }
}
