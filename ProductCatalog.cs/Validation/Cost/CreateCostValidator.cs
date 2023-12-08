using ProductCatalog.API.Models.Cost;

namespace ProductCatalog.API.Validation.Cost
{
    public class CreateCostValidator : AbstractValidator<CreateCostDto>
    {
        public CreateCostValidator()
        {
            RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
            RuleFor(x => x.Price).GreaterThan(0).NotEmpty();
        }
    }
}
