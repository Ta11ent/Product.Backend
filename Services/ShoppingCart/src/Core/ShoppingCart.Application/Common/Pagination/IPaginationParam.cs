namespace ShoppingCart.Application.Common.Pagination
{
    public interface IPaginationParam
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
