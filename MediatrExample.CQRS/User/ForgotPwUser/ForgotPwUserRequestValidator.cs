using FluentValidation;

namespace MediatrExample.CQRS.User.ForgotPwUser
{
    public class ForgotPwUserRequestValidator : AbstractValidator<ForgotPwUserRequest>
    {
        public ForgotPwUserRequestValidator()
        {
            RuleFor(x => x.Mail).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).MaximumLength(100);
        }
    }
}
