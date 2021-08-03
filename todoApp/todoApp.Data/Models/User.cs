using System;
using Info.Data.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace todoApp.Data.Models
{
    public class User: todoAppBaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
