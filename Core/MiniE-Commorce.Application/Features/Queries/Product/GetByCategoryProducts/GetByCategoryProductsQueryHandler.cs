using AutoMapper;
using MediatR;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using MiniE_Commorce.Application.Interfaces.Services.Redis;

namespace MiniE_Commorce.Application.Features.Queries.Product.GetByCategoryProducts
{
    public class GetByCategoryProductsQueryHandler : IRequestHandler<GetByCategoryProductsQueryRequest, List<GetByCategoryProductsQueryResponse>>
    {
        private readonly ICategoryCacheService _categoryCacheService;
        private readonly IProductCacheService _productCacheService;

        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetByCategoryProductsQueryHandler(ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository, IMapper mapper, ICategoryCacheService categoryCacheService, IProductCacheService productCacheService)
        {
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
            _categoryCacheService = categoryCacheService;
            _productCacheService = productCacheService;
        }

        public async Task<List<GetByCategoryProductsQueryResponse>> Handle(GetByCategoryProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var allProducts = new List<GetByCategoryProductsQueryResponse>();

            await RecursivelyGetProducts(request.CategoryId, allProducts);

            return allProducts;
        }

        private async Task RecursivelyGetProducts(int categoryId, List<GetByCategoryProductsQueryResponse> productDtos)
        {
            var product = await _productCacheService.GetAllAsync();
            var categories = await _categoryCacheService.GetAllAsync();

            var products= product.Where(x => x.CategoryId == categoryId);



            //var category = await _categoryReadRepository.GetAsync(predicate:x=>x.Id==categoryId, include:x=>x.Include(p=>p.Products));
            var mappedProductDtos = _mapper.Map<List<GetByCategoryProductsQueryResponse>>(products);
            mappedProductDtos.ForEach(item =>
        item.CategoryName = categories.FirstOrDefault(c => c.Id == item.CategoryId).Name);



            productDtos.AddRange(mappedProductDtos);

            //var subcategories = await _categoryReadRepository.GetAllAsync(predicate: x => x.ParrentCategoryId == categoryId);

            var subcategories = categories.Where(x => x.ParrentCategoryId == categoryId).ToList();

            foreach (var subcategory in (subcategories))
            {
                await RecursivelyGetProducts(subcategory.Id, productDtos);
            }

        }
    }
}
