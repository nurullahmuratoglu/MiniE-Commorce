using AutoMapper;
using MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Features.Queries.Product.GetAllProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product, GetAllProductsQueryResponse>().ReverseMap();
        }
    }
}
