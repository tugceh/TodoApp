using System;
namespace todoApp.Data.Models
{

    public class TodoList : todoAppBaseEntity
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
