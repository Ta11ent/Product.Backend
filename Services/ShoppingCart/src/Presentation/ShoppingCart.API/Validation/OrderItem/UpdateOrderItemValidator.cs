using ShoppingCart.API.Models.ProductRange;

namespace ShoppingCart.API.Validation.ProductRange
{
    public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemDto>
    {
        public UpdateOrderItemValidator() =>
            RuleFor(x => x.Count).NotEmpty().GreaterThan(0).LessThan(100);
    }
}
