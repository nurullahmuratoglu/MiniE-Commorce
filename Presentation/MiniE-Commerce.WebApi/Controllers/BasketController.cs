using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniE_Commorce.Application.Features.Commands.BasketItem.AddBasketItem;
using MiniE_Commorce.Application.Features.Commands.BasketItem.RemoveBasketItem;
using MiniE_Commorce.Application.Features.Commands.BasketItem.UpdateBasketItem;
using MiniE_Commorce.Application.Features.Commands.Product.CreateProduct;
using MiniE_Commorce.Application.Features.Queries.Basket.GetBasketItems;

namespace MiniE_Commerce.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddBasketItem(AddBasketItemCommandRequest request)
        {

            await _mediator.Send(request);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBasktItem(UpdateBasketItemCommandRequest request)
        {

            await _mediator.Send(request);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveBasketItem(RemoveBasketItemCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }        
        [HttpGet]
        public async Task<IActionResult> GetBasket(GetBasketItemsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }


    }
}
