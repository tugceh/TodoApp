using System;
namespace todoApp.Data.Dtos.Register
{
    public class DeleteUserRequest : BaseRequest
    {
        public Guid id { get; set; }
    }

    public class DeleteUserResponse : BaseResponse
    {
    }
}
