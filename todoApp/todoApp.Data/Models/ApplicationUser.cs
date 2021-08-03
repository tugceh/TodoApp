using System;
using Info.Data.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace todoApp.Data.Models
{

    public class ApplicationUser : IdentityUser<Guid>, IBaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int Deleted { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
