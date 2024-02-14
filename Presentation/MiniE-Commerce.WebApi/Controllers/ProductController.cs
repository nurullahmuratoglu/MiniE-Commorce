using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MiniE_Commorce.Application.Features.Queries.Product.GetAllProducts;
using MiniE_Commorce.Application.Features.Queries.Product.GetByIdProduct;
using MiniE_Commorce.Application.Features.Commands.Product.CreateProduct;
using Microsoft.AspNetCore.Authorization;
using MiniE_Commorce.Application.Interfaces.Services.Redis;

namespace MiniE_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductCacheService _productCacheService;

        public ProductController(IMediator mediator, IProductCacheService productCacheService)
        {
            _mediator = mediator;
            _productCacheService = productCacheService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var response = await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetByIdProductQueryRequest() { ProductId = id });
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {

            await _mediator.Send(request);
            return Ok();
        }

    }
}
