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

namespace MiniE_Commorce.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetByIdProductQueryHandler( IMapper mapper, IProductReadRepository productReadRepository)
        {
           
            _mapper = mapper;
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetAsync(predicate:x => x.Id == request.ProductId,include:x=>x.Include(p=>p.Category));
            var prodoctDto = _mapper.Map<Entites.Product, GetByIdProductQueryResponse>(product);
            return prodoctDto;


        }
    }
}
