using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.BasketItem.UpdateBasketItem
{
    public class UpdateBasketItemCommandRequest:IRequest<Unit>
    {
        public int BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
