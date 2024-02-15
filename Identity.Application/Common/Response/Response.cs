using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Response
{
    public class Response<T> : IResponse<T>
    {
        public Response(T data, IEnumerable<IdentityError> errors = null!) {
            this.data = data;
            this.isSuccess = data is null ? false : true;
            this.errors = errors;
        }
        public T data { get; set; }
        public bool isSuccess { get; set; }
        public IEnumerable<IdentityError> errors { get; set; }
    }
}
