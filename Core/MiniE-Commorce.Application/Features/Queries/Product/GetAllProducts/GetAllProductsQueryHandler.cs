using AutoMapper;
using MediatR;
using Entities = MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using MiniE_Commorce.Application.Interfaces.Services.Redis;
using MiniE_Commerce.Domain.Entities;

namespace MiniE_Commorce.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
    {
        private readonly IProductCacheService _productCacheService;
        private readonly ICategoryCacheService _categoryCacheService;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IMapper mapper,IProductCacheService productCacheService, ICategoryCacheService categoryCacheService)
        {
            _mapper = mapper;
            _productCacheService = productCacheService;
            _categoryCacheService = categoryCacheService;
        }

        public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {


            var products = await _productCacheService.GetAllAsync();
            var categories = await _categoryCacheService.GetAllAsync();
            var productresponse = _mapper.Map<IList<GetAllProductsQueryResponse>>(products);
                List<GetAllProductsQueryResponse> productresponseList = new List<GetAllProductsQueryResponse>(productresponse);
            productresponseList.ForEach(item =>
            item.CategoryName = categories.FirstOrDefault(c => c.Id == item.CategoryId).Name
            );
            

            return productresponseList;
        }
    }
}
