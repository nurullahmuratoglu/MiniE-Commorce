using AutoMapper;
using MediatR;
using MiniE_Commorce.Application.Dtos.Basket;
using MiniE_Commorce.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.BasketItem.UpdateBasketItem
{
    public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommandRequest, Unit>
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public UpdateBasketItemCommandHandler(IMapper mapper, IBasketService basketService)
        {
            _mapper = mapper;
            _basketService = basketService;
        }

        public async Task<Unit> Handle(UpdateBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            var basketItemDto = _mapper.Map<UpdateBasketItemDto>(request);

            await _basketService.UpdateQuantityAsync(basketItemDto);

            return Unit.Value;
        }
    }
}
