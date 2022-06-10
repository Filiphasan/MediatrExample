using FluentValidation;

namespace MediatrExample.CQRS.User.GetAllUser
{
    public class GetAllUserRequestValidator : AbstractValidator<GetAllUserRequest>
    {
        public GetAllUserRequestValidator()
        {
            RuleFor(x => x.PageNumber).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageCount).NotNull().GreaterThan(0);
        }
    }
}
