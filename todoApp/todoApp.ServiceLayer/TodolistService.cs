using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using todoApp.Data.Dtos.todolist;
using todoApp.Data.Models;
using todoApp.Repository;
using todoApp.ServiceLayer.Shared;

namespace todoApp.ServiceLayer
{

    public class TodolistService : todoAppService<TodoList, TodolistRepository>, ITodolistService
    {
        private IHostingEnvironment _env;

        public TodolistService(IServiceProvider serviceProvider, IHostingEnvironment env) : base(serviceProvider)
        {
            _env = env;
        }

        public async Task<List<TodoList>> GetAllTodosByUser(Guid userid)
        {
            var todolist = Repository.GetAllTodosByUser(userid);
            return todolist;
        }

        public async Task<List<TodoList>> GetAllTodos()
        {
            var todolist = Repository.GetAllTodos();
            return todolist;
        }

        public async Task<AddTodoListResponse> AddTodo(AddTodoListRequest addRequest)
        {
            var result = new AddTodoListResponse { isSuccess = true };
            try
            {
                TodoList newTodo = new TodoList();
                newTodo.Name = addRequest.Name;
                newTodo.CreatedDate = DateTime.Now;
                newTodo.LastUpdatedDate = DateTime.Now;
                newTodo.Time = addRequest.Time;
                newTodo.UserId = addRequest.UserId;
                newTodo.Deleted = 0;

                Repository.Insert(newTodo);
                await UnitOfWork.SaveChangesAsync();
                result.isSuccess = true;
                result.message = "Todo has been added successfully";

            }
            catch (Exception e)
            {
                result.isSuccess = false;
                result.message = e.Message;

            }
            return result;
        }

        public async Task<DeleteTodoListResponse> DeleteTodo(DeleteTodoListRequest request)
        {
            var result = new DeleteTodoListResponse { isSuccess = true };

            try
            {
                var user = Repository.FindTodoById(request.id);
                user.LastUpdatedDate = DateTime.Now;
                user.Deleted = 1;
                Update(user);

                await UnitOfWork.SaveChangesAsync();
                result.isSuccess = true;
                result.message = "Todo has been deleted successfully";
            }
            catch (Exception e)
            {
                result.isSuccess = false;
                result.message = e.Message;
            }

            return result;
        }
    }
}
