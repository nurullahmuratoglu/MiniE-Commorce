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

namespace MiniE_Commorce.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
    {

        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IMapper mapper, IProductReadRepository productReadRepository)
        {
            _mapper = mapper;
            _productReadRepository = productReadRepository;
        }

        public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productReadRepository.GetAllAsync(include: x => x.Include(p => p.Category));


            var productresponse = _mapper.Map<IList<Entities.Product>, IList<GetAllProductsQueryResponse>>(products);
            return productresponse;
        }
    }
}
