using System;
namespace todoApp.Data.Dtos.todolist
{
    public class DeleteTodoListRequest : BaseRequest
    {
        public Guid id { get; set; }
    }

    public class DeleteTodoListResponse : BaseResponse
    {
    }
}
