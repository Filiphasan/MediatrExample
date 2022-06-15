using FluentValidation;
using MediatrExample.Shared.CustomMethod;

namespace MediatrExample.CQRS.User.AddUser
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).MaximumLength(100);
            RuleFor(x => x.Gsm).NotNull().NotEmpty().Must(x =>
             {
                 string result = x.FormatGSMForTR();
                 x = result;
                 return result != string.Empty;
             }).WithMessage("GSM Number has Wrong Format! Correct Format One Of '+905555555555,905555555555,05555555555,5555555555'");
            RuleFor(x => x.Password).CheckMyPassword();
        }
    }
}
