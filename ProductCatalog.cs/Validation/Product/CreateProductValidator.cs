using ProductCatalog.cs.Models.Product;

namespace ProductCatalog.APIcs.Validation.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Price).GreaterThan(0).NotEmpty();
        }
    }
}