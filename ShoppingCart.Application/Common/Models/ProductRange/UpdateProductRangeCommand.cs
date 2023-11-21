namespace ShoppingCart.Application.Common.Models.ProductRange
{
    public class UpdateProductRangeCommand
    {
        public Guid OrderId { get; set; }
        public Guid ProductRangeId { get; set; }
        public int Count { get; set; }
    }
}
