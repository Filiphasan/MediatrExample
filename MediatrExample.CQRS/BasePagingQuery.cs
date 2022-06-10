using MediatR;
using MediatrExample.Shared.DataModels;
using MediatrExample.Shared.RequestResponse.Pagination;

namespace MediatrExample.CQRS
{
    public class BasePagingQuery<T> : PaginationRequest, IRequest<GenericResponse<T>>
    {
    }
}
