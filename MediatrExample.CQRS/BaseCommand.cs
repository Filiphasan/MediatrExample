using MediatR;
using MediatrExample.Shared.RequestResponse;

namespace MediatrExample.CQRS
{
    public class BaseCommand<T> : IRequest<GenericResponse<T>>
    {
    }
}
