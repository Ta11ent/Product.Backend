using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.Application.Common.Response
{
    internal class Response<T> : IResponse<T> where T : class
    {
        public Response(T _data) {
            data = _data;
            isSuccess = _data is null ? false : true;
        }
        public T data { get; set; }
        public bool isSuccess { get; set; }

    }
}
