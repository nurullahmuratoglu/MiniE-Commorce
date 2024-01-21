using MediatR;
using Entites = MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using MiniE_Commorce.Application.Interfaces.Services.Redis;

namespace MiniE_Commorce.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductCacheService _productCacheService;
        private readonly ICategoryCacheService _categoryCacheService;
        private readonly IMapper _mapper;

        public GetByIdProductQueryHandler(IMapper mapper, IProductCacheService productCacheService, ICategoryCacheService categoryCacheService)
        {

            _mapper = mapper;
            _productCacheService = productCacheService;
            _categoryCacheService = categoryCacheService;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productCacheService.GetAsync(request.ProductId.ToString());
            var category = await _categoryCacheService.GetAsync(product.CategoryId.ToString());
            var productResponse = _mapper.Map<GetByIdProductQueryResponse>(product);
            productResponse.CategoryName = category.Name;
            return productResponse;


        }
    }
}
