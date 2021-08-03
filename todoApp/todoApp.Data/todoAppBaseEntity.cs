using System;
using System.ComponentModel.DataAnnotations;
using Info.Data.BaseEntity;

namespace todoApp.Data
{
    public class todoAppBaseEntity : BaseEntity
    {
        public todoAppBaseEntity()
        {
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
            Deleted = 0;
        }
        public todoAppBaseEntity(Guid userId)
        {
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
            Deleted = 0;
        }
        [Key]
        public Guid Id { get; set; }
    }
}
