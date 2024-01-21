using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniE_Commorce.Application.Features.Commands.Category;
using MiniE_Commorce.Application.Features.Commands.Product.CreateProduct;
using MiniE_Commorce.Application.Features.Queries.Category.GetAllCategory;
using MiniE_Commorce.Application.Features.Queries.Product.GetByCategoryProducts;
using MiniE_Commorce.Application.Interfaces.Services.Redis;

namespace MiniE_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoryCacheService _categoryCacheService;

        public CategoryController(IMediator mediator, ICategoryCacheService categoryCacheService)
        {
            _mediator = mediator;
            _categoryCacheService = categoryCacheService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest request)
        {

            await _mediator.Send(request);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {

            var response = await _mediator.Send(new GetAllCategoryQueryRequest());
            return Ok(response);
        }


        [HttpGet("{categoryid}/products")]

        public async Task<IActionResult> GetProductByCategoryId(int categoryid)
        {

            var response = await _mediator.Send(new GetByCategoryProductsQueryRequest() { CategoryId = categoryid });
            return Ok(response);
        }
    }
}
