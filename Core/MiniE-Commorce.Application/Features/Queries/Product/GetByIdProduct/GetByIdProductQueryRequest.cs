using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryRequest:IRequest<GetByIdProductQueryResponse>
    {
        public int ProductId { get; set; }
    }
}
