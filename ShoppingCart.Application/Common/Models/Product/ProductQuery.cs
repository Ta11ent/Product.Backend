using System.Text;

namespace ShoppingCart.Application.Common.Models.Product
{
    public class ProductQuery
    {
        public ProductQuery(List<Guid> guids) {
            StringBuilder sb = new StringBuilder(guids.Count);

            for(int i = 0; i < guids.Count; i++)
                sb.Append(i > 0
                    ? $"&{nameof(ProductId)}={guids[i]}"
                    : $"{nameof(ProductId)}={guids[i]}");

            ProductId = sb.ToString();
        }
        public string ProductId { get; private set; }
    }
}
