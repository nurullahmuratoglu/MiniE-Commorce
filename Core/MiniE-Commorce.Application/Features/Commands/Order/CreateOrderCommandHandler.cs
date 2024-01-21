using MediatR;
using MiniE_Commorce.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.Order
{
    
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, Unit>
    {
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Unit> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CreateOrder();
            return Unit.Value;
        }
    }
}
