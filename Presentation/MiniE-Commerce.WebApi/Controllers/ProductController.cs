using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MiniE_Commorce.Application.Features.Queries.Product.GetAllProducts;

namespace MiniE_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var response=await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(response);
        }
    }
}
