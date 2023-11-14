﻿using FluentValidation;
using ProductCatalog.cs.Models.Product;

namespace ProductCatalog.APIcs.Validation.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator() 
        {
            RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
        }
    }
}
