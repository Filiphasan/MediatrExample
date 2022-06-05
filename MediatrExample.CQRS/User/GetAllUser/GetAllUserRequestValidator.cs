using FluentValidation;

namespace MediatrExample.CQRS.User.GetAllUser
{
    public class GetAllUserRequestValidator : AbstractValidator<GetAllUserRequest>
    {
        public GetAllUserRequestValidator()
        {

        }
    }
}
