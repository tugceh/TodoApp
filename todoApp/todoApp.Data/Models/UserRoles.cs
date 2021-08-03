using System;
namespace todoApp.Data.Models
{
    public class UserRoles : todoAppBaseEntity
    {
        public string RoleName { get; set; }
        public long UserId { get; set; }

    }
}
