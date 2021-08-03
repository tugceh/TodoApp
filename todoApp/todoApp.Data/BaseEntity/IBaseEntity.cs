using System;
namespace Info.Data.BaseEntity
{
    public interface IBaseEntity
    {
        //long Id { get; set; }
        int Deleted { get; set; }
        DateTime LastUpdatedDate { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
