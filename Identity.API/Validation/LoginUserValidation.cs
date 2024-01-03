﻿using Identity.API.Models;

namespace Identity.API.Validation
{
    public class LoginUserValidation : AbstractValidator<LoginDto>
    {
        public LoginUserValidation() {
            RuleFor(x => x.UserName).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(8)
                .MaximumLength(16)
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.\-]+").WithMessage("Your password must contain at least one (-!? *.).");

        }
    }
}
