namespace ProductCatalog.Application.Common.Pagination
{
    public interface IPagination
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
