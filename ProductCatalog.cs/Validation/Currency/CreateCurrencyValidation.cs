using ProductCatalog.API.Models.Currecny;

namespace ProductCatalog.API.Validation.Currency
{
    public class CreateCurrencyValidation : AbstractValidator<CreateCurrencyDto>
    {
        public CreateCurrencyValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(3);
        }
    }
}
