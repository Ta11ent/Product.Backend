using ShoppingCart.API.Models.ProductRange;

namespace ShoppingCart.API.Validation.ProductRange
{
    public class UpdateProductRangeValidator : AbstractValidator<UpdateProductRnageDto>
    {
        public UpdateProductRangeValidator() =>
            RuleFor(x => x.Count).NotEmpty().GreaterThan(0).LessThan(100);
    }
}
