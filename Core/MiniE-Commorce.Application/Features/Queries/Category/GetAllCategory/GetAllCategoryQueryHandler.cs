using MediatR;
using Microsoft.EntityFrameworkCore;
using Entities = MiniE_Commerce.Domain.Entities;
using  MiniE_Commorce.Application.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using AutoMapper;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Services.Redis;

namespace MiniE_Commorce.Application.Features.Queries.Category.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, GetAllCategoryQueryResponse>
    {
        private readonly ICategoryCacheService _categoryCacheService;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IMapper mapper, ICategoryReadRepository categoryReadRepository, ICategoryCacheService categoryCacheService)
        {
            _mapper = mapper;
            _categoryReadRepository = categoryReadRepository;
            _categoryCacheService = categoryCacheService;
        }

        public async Task<GetAllCategoryQueryResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var baseCategory = await _categoryCacheService.GetAsync("1");

            var categoryTree = BuildCategoryTree(baseCategory);

            return categoryTree;


        }
        private  GetAllCategoryQueryResponse BuildCategoryTree(Entities.Category category)
        {
            var categoryDto = _mapper.Map<GetAllCategoryQueryResponse>(category);

          
            var subcategories = _categoryCacheService.GetAllAsync().Result;
            subcategories = subcategories.Where(c => c.ParrentCategoryId == category.Id).OrderBy(x => x.Name).ToList();

            categoryDto.Subcategories = subcategories.Select(BuildCategoryTree).ToList();

            return categoryDto;
        }
    }
}
