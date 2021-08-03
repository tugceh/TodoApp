using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Info.Repository;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using todoApp.Data;
using todoApp.Data.Models;

namespace todoApp.Repository
{

    public class TodolistRepository : Repository<TodoList>, ITodolistRepository
    {
        private IHostingEnvironment _env;

        public TodolistRepository(ApplicationDbContext context, IServiceProvider serviceProvider, IHostingEnvironment env) : base(context, serviceProvider)
        {
            _env = env;
        }

        public List<TodoList> GetAllTodosByUser(Guid userid)
        {
            return QueryableActive().Where(i => i.UserId == userid).ToList();
        }

        public List<TodoList> GetAllTodos()
        {
            return QueryableActive().ToList();
        }

        public TodoList FindTodoById(Guid Id)
        {
            var result = QueryableActive().First(x => x.Id == Id);

            return result;
        }

    }
}
