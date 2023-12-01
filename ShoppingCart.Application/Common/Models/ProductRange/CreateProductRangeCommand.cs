namespace ShoppingCart.Application.Common.Models.ProductRange
{
    public class CreateProductRangeCommand
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Count { get; set; }
    }
}
