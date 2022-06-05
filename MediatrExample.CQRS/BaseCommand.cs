using MediatR;
using MediatrExample.Shared.DataModels;

namespace MediatrExample.CQRS
{
    public class BaseCommand<T> : IRequest<GenericResponse<T>>
    {
    }
}
