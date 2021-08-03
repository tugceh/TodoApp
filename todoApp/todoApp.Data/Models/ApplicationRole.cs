using System;
using System.Collections.Generic;
using Info.Data.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace todoApp.Data.Models
{

    public class ApplicationRole : IdentityRole<Guid>, IBaseEntity
    {
        public string Name { get; set; }
        public int Deleted { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

    }
}
