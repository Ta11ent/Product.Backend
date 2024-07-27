using ProductCatalog.cs.Models.Product;

namespace ProductCatalog.APIcs.Validation.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Price).GreaterThan(0).NotEmpty();
        }
    }
}
