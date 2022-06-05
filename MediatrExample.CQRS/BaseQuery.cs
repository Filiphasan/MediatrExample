using MediatR;
using MediatrExample.Shared.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatrExample.CQRS
{
    public class BaseQuery<T> : IRequest<GenericResponse<T>>
    {
    }
}
