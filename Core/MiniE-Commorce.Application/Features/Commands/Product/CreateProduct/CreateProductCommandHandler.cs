using AutoMapper;
using MediatR;
using Entities = MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using Microsoft.Extensions.Logging;
using MiniE_Commorce.Application.Interfaces.Services.Redis;

namespace MiniE_Commorce.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductCacheService _cacheService;

        private readonly IProductWriteRepository _productWriteRepository;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IProductWriteRepository productWriteRepository, ILogger<CreateProductCommandHandler> logger, IProductCacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productWriteRepository = productWriteRepository;
            _logger = logger;
            _cacheService = cacheService;
        }


        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<CreateProductCommandRequest, Entities.Product>(request);
            await _productWriteRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();
            await _cacheService.SetAsync(product.Id.ToString(), product);
            _logger.LogInformation("product eklendi");
            return Unit.Value;

        }
    }
}
