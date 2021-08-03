using System;
using Info;

namespace todoApp.Data.Dtos.todolist
{

    public class AddTodoListRequest : BaseRequest
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public Guid UserId { get; set; }
    }

    public class AddTodoListResponse : BaseResponse
    {

    }
}
