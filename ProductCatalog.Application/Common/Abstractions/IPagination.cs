namespace ProductCatalog.Application.Common.Abstractions
{
    public interface IPagination
    {
        int page { get; set; }
        int pageSize { get; set; }
    }
}
