namespace ShoppingCart.Application.Common.Models.ProductRange
{
    public class CreateProductRangeCommand
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
