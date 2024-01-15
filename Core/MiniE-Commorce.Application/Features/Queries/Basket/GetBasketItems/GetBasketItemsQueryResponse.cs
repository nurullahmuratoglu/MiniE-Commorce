using MiniE_Commorce.Application.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryResponse
    {
        public int Id { get; set; }
        public List<GetBasketItemDto> BasketItems { get; set; }
        public float TotalPrice { get; set; }
    }
}
