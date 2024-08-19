using ShoppingCart.API.Models.Order;

namespace ShoppingCart.API.Validation.Order
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
