using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Response
{
    public class Response<T>
    {
        public Response(T data) { 
            this.data = data;
            this.isSuccess = data is null ? false : true;
        }
        public T data { get; set; }
        public bool isSuccess { get; set; }
    }
}
