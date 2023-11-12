using ProductCatalog.Application.Common.Abstractions;
using System.Collections;

namespace ProductCatalog.Application.Common.Response
{
    public class PageResponse<T> : IResponse<T> where T : IList
    {
        public PageResponse(T _data, IPagination pagination) { 

            data = _data;
            meta = new Meta
            {
                count = _data.Count,
                page = pagination.Page,
                pageSize = pagination.PageSize,
            };
            isSuccess = _data is null ? false : true;

        }
        public T data { get; set; }
        public Meta meta { get; set; }
        public bool isSuccess { get; set; }
        
    }
    public class Meta
    {
        public int count { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }

    }
}
