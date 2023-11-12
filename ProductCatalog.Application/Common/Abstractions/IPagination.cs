namespace ProductCatalog.Application.Common.Abstractions
{
    public interface IPagination
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
