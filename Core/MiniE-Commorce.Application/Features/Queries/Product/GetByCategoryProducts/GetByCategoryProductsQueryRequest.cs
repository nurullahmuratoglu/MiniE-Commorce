using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Queries.Product.GetByCategoryProducts
{
    public class GetByCategoryProductsQueryRequest:IRequest<List<GetByCategoryProductsQueryResponse>>
    {
        public int CategoryId { get; set; }
    }
}
