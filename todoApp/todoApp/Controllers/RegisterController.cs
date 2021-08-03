using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Info;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using todoApp.Data.Dtos.Register;
using todoApp.Data.Models;
using todoApp.ServiceLayer;


namespace todoApp.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : BaseApiController<RegisterController, ApplicationUser, IRegisterService>
    {
        public RegisterController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        [HttpPost("addUser")]
        public async Task<IActionResult> Register([FromBody] AddUserRequest addUserRequest)
        {
            return Ok(await Service.AddUser(addUserRequest));
        }

        [HttpGet("listUsers")]
        public async Task<IActionResult> GetListUsers()
        {
            return Ok(await Service.GetAllUsers());
        }

        [HttpPost("deleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest deleteRequest)
        {
            return Ok(await Service.DeleteUser(deleteRequest));
        }
    }
}
