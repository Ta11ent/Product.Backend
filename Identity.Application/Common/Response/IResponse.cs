using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Response
{
    internal interface IResponse<T>
    {
        T data { get; set; }
        bool isSuccess { get; set; }
        //IEnumerable<IdentityError> errors { get; set; }

    }
}
