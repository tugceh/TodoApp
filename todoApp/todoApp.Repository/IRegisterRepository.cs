using System;
using System.Collections.Generic;
using Info.Repository;
using todoApp.Data.Models;

namespace todoApp.Repository
{
    public interface IRegisterRepository : IRepository<ApplicationUser>
    {
        List<ApplicationUser> GetApplicationUsers();
        ApplicationUser FindApplicationUserById(Guid Id);
    }
}
