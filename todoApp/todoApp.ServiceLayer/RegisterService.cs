using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using todoApp.Data.Dtos.Register;
using todoApp.Data.Models;
using todoApp.Repository;
using todoApp.ServiceLayer.Shared;

namespace todoApp.ServiceLayer
{
    public class RegisterService : todoAppService<ApplicationUser, RegisterRepository>, IRegisterService
    {

        public RegisterService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            var users = Repository.GetApplicationUsers();

            return users;

        }

        public async Task<AddUserResponse> AddUser(AddUserRequest addUserRequest)
        {
            var result = new AddUserResponse { isSuccess = true };
            try
            {
                ApplicationUser newUser = new ApplicationUser();
                newUser.Name = addUserRequest.Name;
                newUser.Email = addUserRequest.Email;
                newUser.Password = addUserRequest.Password;
                newUser.CreatedDate = DateTime.Now;
                newUser.LastUpdatedDate = DateTime.Now;
                newUser.Deleted = 0;

                Repository.Insert(newUser);
                await UnitOfWork.SaveChangesAsync();
                result.isSuccess = true;
                result.message = "User has been added successfully";

            }
            catch (Exception e)
            {
                result.isSuccess = false;
                result.message = e.Message;
            }
            return result;
        }

        public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request)
        {
            var result = new DeleteUserResponse { isSuccess = true };

            try
            {
                var user = Repository.FindApplicationUserById(request.id);
                user.LastUpdatedDate = DateTime.Now;
                user.Deleted = 1;
                Update(user);

                await UnitOfWork.SaveChangesAsync();
                result.isSuccess = true;
                result.message = "User has been deleted successfully";
            }
            catch (Exception e)
            {
                result.isSuccess = false;
                result.message = e.Message;
            }

            return result;
        }
    }
}
