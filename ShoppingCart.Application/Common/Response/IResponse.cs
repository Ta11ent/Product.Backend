namespace ShoppingCart.Application.Common.Response
{
    internal interface IResponse<T>
    {
        T data { get; set; }
        bool isSuccess { get; set; }
    }
}
