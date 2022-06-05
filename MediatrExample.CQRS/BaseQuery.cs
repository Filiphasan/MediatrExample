using MediatR;
using MediatrExample.Shared.DataModels;

namespace MediatrExample.CQRS
{
    public class BaseQuery<T> : IRequest<GenericResponse<T>>
    {
    }
}
