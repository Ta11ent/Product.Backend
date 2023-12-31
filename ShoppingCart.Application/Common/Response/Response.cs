﻿namespace ShoppingCart.Application.Common.Response
{
    public class Response<T> : IResponse<T> where T : class
    {
        public Response(T _data)
        {
            data = _data;
            isSuccess = _data is null ? false : true;
        }
        public T data { get; set; }
        public bool isSuccess { get; set; }

    }
}
