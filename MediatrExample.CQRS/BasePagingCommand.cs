using MediatR;
using MediatrExample.Shared.DataModels;
using MediatrExample.Shared.RequestResponse.Pagination;

namespace MediatrExample.CQRS
{
    public class BasePagingCommand<T> : PaginationRequest, IRequest<GenericResponse<T>>
    {
    }
}
