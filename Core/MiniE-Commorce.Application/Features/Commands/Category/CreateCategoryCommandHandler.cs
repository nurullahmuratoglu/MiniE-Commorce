using AutoMapper;
using MediatR;
using  Entities= MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Services.Redis;

namespace MiniE_Commorce.Application.Features.Commands.Category
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly ICategoryCacheService _categoryCacheService;

        public CreateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ICategoryWriteRepository categoryWriteRepository, ICategoryCacheService categoryCacheService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryWriteRepository = categoryWriteRepository;
            _categoryCacheService = categoryCacheService;
        }

        public async Task<Unit> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.ParrentCategoryId is null) request.ParrentCategoryId = 1;
            var category = _mapper.Map<CreateCategoryCommandRequest, Entities.Category>(request);
            await _categoryWriteRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();
            await _categoryCacheService.SetAsync(category.Id.ToString(),category);
            return Unit.Value;
        }
    }
}
