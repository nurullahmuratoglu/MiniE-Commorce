using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Queries.Order
{
    public class GetOrderQueryRequest:IRequest<GetOrderQueryResponse>
    {
    }
}
