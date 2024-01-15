using AutoMapper;
using MediatR;
using MiniE_Commorce.Application.Dtos.Basket;
using MiniE_Commorce.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, GetBasketItemsQueryResponse>
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public GetBasketItemsQueryHandler(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<GetBasketItemsQueryResponse> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var basket=await _basketService.GetBasketItemsAsync();
            var basketDto = _mapper.Map<GetBasketItemsQueryResponse>(basket);


            basketDto.TotalPrice = CalculateTotalPrice(basketDto.BasketItems);

            return basketDto;
        }

        private float CalculateTotalPrice(List<GetBasketItemDto> basketItems)
        {
            return basketItems?.Sum(item => item.Quantity * item.ProductPrice) ?? 0;
        }

    }
}
