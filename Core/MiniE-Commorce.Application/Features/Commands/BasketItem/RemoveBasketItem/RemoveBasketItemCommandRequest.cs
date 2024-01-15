using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.BasketItem.RemoveBasketItem
{
    public class RemoveBasketItemCommandRequest:IRequest<Unit>
    {
        public int BasketItemId { get; set; }
    }
}
