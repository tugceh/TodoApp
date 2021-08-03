using System;
using System.Collections.Generic;
using Info.Repository;
using todoApp.Data.Models;

namespace todoApp.Repository
{
    public interface ITodolistRepository : IRepository<TodoList>
    {
        List<TodoList> GetAllTodosByUser(Guid userid);
        List<TodoList> GetAllTodos();
        TodoList FindTodoById(Guid Id);
    }
}
