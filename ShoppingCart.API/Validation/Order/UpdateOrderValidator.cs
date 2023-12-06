using ShoppingCart.API.Models.Order;

namespace ShoppingCart.API.Validation.Order
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.OrderId).NotEqual(Guid.Empty);
            RuleFor(x => x.IsPaid).NotEmpty();
        }
    }
}
