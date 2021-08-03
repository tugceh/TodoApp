using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Info.Initializations;
using todoApp.Data.Dtos.Register;
using todoApp.Data.Models;

namespace todoApp.ServiceLayer
{
    public interface IRegisterService : IService<ApplicationUser>
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<AddUserResponse> AddUser(AddUserRequest addUserRequest);
        Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request);
    }
}
