namespace ProductCatalog.Application.Common.Abstractions
{
    internal interface IResponse<T>
    {
        T data { get; set; }
        bool isSuccess { get; set; }

    }
}
