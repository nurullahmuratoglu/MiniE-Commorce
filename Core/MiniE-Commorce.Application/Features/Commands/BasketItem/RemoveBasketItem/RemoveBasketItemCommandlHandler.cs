using MediatR;
using MiniE_Commorce.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.BasketItem.RemoveBasketItem
{
    public class RemoveBasketItemCommandlHandler : IRequestHandler<RemoveBasketItemCommandRequest, Unit>
    {
        private readonly IBasketService _basketService;

        public RemoveBasketItemCommandlHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<Unit> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
        {

            await _basketService.RemoveBasketItemAsync(request.BasketItemId);
            return Unit.Value;
        }
    }
}
