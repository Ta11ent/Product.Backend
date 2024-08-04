using ProductCatalog.API.Models.Manufacturer;

namespace ProductCatalog.API.Validation.Manufacturer
{
    public class CreateManufacturerValidator : AbstractValidator<CreateManufacturerDto>
    {
        public CreateManufacturerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
        }
    }
}
