﻿using ShoppingCart.Application.Common.Pagination;
using System.Collections;

namespace ShoppingCart.Application.Common.Response
{
    public class PageResponse<T> : IResponse<T> where T : IList
    {
        public PageResponse(T _data, IPaginationParam pagination)
        {

            data = _data;
            meta = pagination is null
                ? default
                : new Meta
                    {
                        count = _data.Count,
                        page = pagination.Page,
                        pageSize = pagination.PageSize,
                    };
            isSuccess = _data is null ? false : true;
        }

        public T data { get; set; }
        public Meta? meta { get; set; }
        public bool isSuccess { get; set; }

    }
    public class Meta
    {
        public int count { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }

    }
}
