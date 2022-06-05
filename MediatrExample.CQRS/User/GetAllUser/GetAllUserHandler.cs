using MediatR;
using MediatrExample.Shared.CustomExceptions;
using MediatrExample.Shared.RequestResponse;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatrExample.CQRS.User.GetAllUser
{
    public class GetAllUserHandler : BaseHandler<GetAllUserRequest, GenericResponse<GetAllUserResponse>>, IRequestHandler<GetAllUserRequest, GenericResponse<GetAllUserResponse>>
    {
        public GetAllUserHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<FluentValidation.IValidator<GetAllUserRequest>> validators) : base(httpContextAccessor, validators)
        {
        }

        public async Task<GenericResponse<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var validErr = await CheckValidate(request);
            if (validErr.Any()) throw new ValidationException(validErr);

            throw new NotImplementedException();
        }
    }
}
