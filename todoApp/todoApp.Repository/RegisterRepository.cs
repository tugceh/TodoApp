using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Info.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using todoApp.Data;
using todoApp.Data.Models;

namespace todoApp.Repository
{
    public class RegisterRepository : Repository<ApplicationUser>, IRegisterRepository
    {

        public RegisterRepository(ApplicationDbContext context, IServiceProvider serviceProvider) : base(context,serviceProvider)
        {
        }

        public List<ApplicationUser> GetApplicationUsers()
        {
            return QueryableActive().ToList();
        }

        public ApplicationUser FindApplicationUserById(Guid Id)
        {
            var result = QueryableActive().First(x => x.Id == Id);

            return result;
        }

    }
}
