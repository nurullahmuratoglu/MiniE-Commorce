using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniE_Commorce.Application.Features.Commands.Order;
using MiniE_Commorce.Application.Features.Queries.Order;

namespace MiniE_Commerce.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder() 
        {
            await _mediator.Send(new CreateOrderCommandRequest());
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
           var response= await _mediator.Send(new GetOrderQueryRequest());
            return Ok(response);
        }
    }
}
