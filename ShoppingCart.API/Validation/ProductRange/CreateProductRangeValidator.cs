using ShoppingCart.API.Models.ProductRange;

namespace ShoppingCart.API.Validation.ProductRange
{
    public class CreateProductRangeValidator : AbstractValidator<CreateProductRangeDto>
    {
        public CreateProductRangeValidator() { 
            RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
            RuleFor(x => x.Count).NotEmpty().GreaterThan(0).LessThan(100);
        }
    }
}
