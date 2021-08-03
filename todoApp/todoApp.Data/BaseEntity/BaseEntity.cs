using System;
using System.ComponentModel.DataAnnotations;

namespace Info.Data.BaseEntity
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public long Id { get; set; }
        public int Deleted { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
