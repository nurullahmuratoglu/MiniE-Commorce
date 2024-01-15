using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using MiniE_Commorce.Application.Dtos.Basket;
using MiniE_Commorce.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.BasketItem.AddBasketItem
{
    public class AddBasketItemCommandHandler : IRequestHandler<AddBasketItemCommandRequest, Unit>
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public AddBasketItemCommandHandler(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            var basketItemDto = _mapper.Map<AddBasketItemDto>(request);
            await _basketService.AddItemToBasketAsync(basketItemDto);

            return Unit.Value;
        }
    }
}
