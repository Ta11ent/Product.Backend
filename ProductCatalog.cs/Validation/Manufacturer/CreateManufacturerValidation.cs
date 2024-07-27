using ProductCatalog.API.Models.Manufacturer;

namespace ProductCatalog.API.Validation.Manufacturer
{
    public class CreateManufacturerValidation : AbstractValidator<CreateManufacturerDto>
    {
        public CreateManufacturerValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
        }
    }
}
