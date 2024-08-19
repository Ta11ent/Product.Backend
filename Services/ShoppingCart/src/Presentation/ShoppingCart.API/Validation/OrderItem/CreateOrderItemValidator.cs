using ShoppingCart.API.Models.ProductRange;

namespace ShoppingCart.API.Validation.ProductRange
{
    public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemDto>
    {
        public CreateOrderItemValidator() { 
            RuleFor(x => x.ProductSaleId).NotEqual(Guid.Empty).NotEmpty();
            RuleFor(x => x.UserId).NotEqual(Guid.Empty).NotEmpty();
            RuleFor(x => x.Count).NotEmpty().GreaterThan(0).LessThan(100);
        }
    }
}
