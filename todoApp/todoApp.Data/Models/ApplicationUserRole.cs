using System;
using Info.Data.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace todoApp.Data.Models
{
    public class ApplicationUserRole : IdentityUserRole<Guid>, IBaseEntity
    {
        public int Id { get; set; }
        public int Deleted { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
