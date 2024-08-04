using ProductCatalog.API.Models.Manufacturer;

namespace ProductCatalog.API.Validation.Manufacturer
{
    public class UpdateManufacturerValidator : AbstractValidator<UpdateManufacturerDto>
    {
        public UpdateManufacturerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
        }
    }
}
