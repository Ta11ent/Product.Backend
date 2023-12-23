using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Response
{
    public class Response<T> : IResponse<T>
    {
        public Response(T _data, IEnumerable<IdentityError> _errors) {
            data = _data;
            isSuccess = _data is null ? false : true;
            errors = _errors;
            
        }
        public T data { get; set; }
        public bool isSuccess { get; set; }
        public IEnumerable<IdentityError> errors { get; set; }
    }
}
