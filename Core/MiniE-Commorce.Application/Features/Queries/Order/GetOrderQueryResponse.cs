using MiniE_Commorce.Application.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Queries.Order
{
    public class GetOrderQueryResponse
    {
        public int Id { get; set; }
        public ICollection<OrderDetailsDto> OrderDetails { get; set; }
        public DateTime? CreatedDate { get; set; }
        public float TotalPrice { get; set; }

    }
}
