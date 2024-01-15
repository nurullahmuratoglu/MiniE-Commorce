using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.BasketItem.AddBasketItem
{
    public class AddBasketItemCommandRequest : IRequest<Unit>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
