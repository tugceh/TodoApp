using System;

namespace todoApp.Data.Dtos.Register
{
    public class AddUserRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AddUserResponse : BaseResponse
    {

    }
}
