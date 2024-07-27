using ProductCatalog.API.Models.Currecny;

namespace ProductCatalog.API.Validation.Currency
{
    public class UpdateCurrencyValidation : AbstractValidator<UpdateCurrencyDto>
    {
        public UpdateCurrencyValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(3);
        }
    }
}
