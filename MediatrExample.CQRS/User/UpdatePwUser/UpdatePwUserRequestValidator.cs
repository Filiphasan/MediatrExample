using FluentValidation;
using MediatrExample.Shared.CustomMethod;

namespace MediatrExample.CQRS.User.UpdatePwUser
{
    public class UpdatePwUserRequestValidator : AbstractValidator<UpdatePwUserRequest>
    {
        public UpdatePwUserRequestValidator()
        {
            RuleFor(x => x.OldPassword).NotNull().NotEmpty();
            RuleFor(x => x.NewPassword).CheckMyPassword();
        }
    }
}
