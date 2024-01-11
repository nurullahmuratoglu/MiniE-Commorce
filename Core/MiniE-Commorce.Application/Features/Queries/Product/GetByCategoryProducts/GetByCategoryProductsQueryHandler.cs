using AutoMapper;
using AutoMapper.Internal.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Entities = MiniE_Commerce.Domain.Entities;
using MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Queries.Product.GetByCategoryProducts
{
    public class GetByCategoryProductsQueryHandler : IRequestHandler<GetByCategoryProductsQueryRequest, List<GetByCategoryProductsQueryResponse>>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetByCategoryProductsQueryHandler(ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository, IMapper mapper)
        {
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<List<GetByCategoryProductsQueryResponse>> Handle(GetByCategoryProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var allProducts = new List<GetByCategoryProductsQueryResponse>();

            await RecursivelyGetProducts(request.CategoryId, allProducts);

            return allProducts;
        }

        private async Task RecursivelyGetProducts(int categoryId, List<GetByCategoryProductsQueryResponse> productDtos)
        {

            var category = await _categoryReadRepository.GetAsync(predicate:x=>x.Id==categoryId, include:x=>x.Include(p=>p.Products));

            //var category = await _context.Categories.Include(x => x.Products.Where(x => x.IsActive == true)).FirstOrDefaultAsync(x => x.Id == categoryId);


            var mappedProductDtos = _mapper.Map<List<GetByCategoryProductsQueryResponse>>(category.Products);
            productDtos.AddRange(mappedProductDtos);

            var subcategories = await _categoryReadRepository.GetAllAsync(predicate: x => x.ParrentCategoryId == categoryId);
            



            //var subcategories = await _context.Categories
            //    .Where(c => c.ParentCategoryID == categoryId)
            //    .ToListAsync();

            foreach (var subcategory in (subcategories))
            {
                await RecursivelyGetProducts(subcategory.Id, productDtos);
            }

        }
    }
}
