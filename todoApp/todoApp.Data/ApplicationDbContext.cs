using System;
using Microsoft.EntityFrameworkCore;
using todoApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace todoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<TodoList> TodoList { get; set; }
    }
}
