using System;
namespace todoApp.Data
{
    public class BaseResponse
    {
        public bool isSuccess { get; set; } = true;
        public string message { get; set; }
    }
}
