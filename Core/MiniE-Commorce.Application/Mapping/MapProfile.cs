using AutoMapper;
using MiniE_Commerce.Domain.Entities;
using MiniE_Commerce.Domain.Entities.Identity;
using MiniE_Commorce.Application.Dtos.User;
using MiniE_Commorce.Application.Features.Commands.AppUser.CreateUser;
using MiniE_Commorce.Application.Features.Commands.Category;
using MiniE_Commorce.Application.Features.Commands.Product.CreateProduct;
using MiniE_Commorce.Application.Features.Queries.Category.GetAllCategory;
using MiniE_Commorce.Application.Features.Queries.Product.GetAllProducts;
using MiniE_Commorce.Application.Features.Queries.Product.GetByCategoryProducts;
using MiniE_Commorce.Application.Features.Queries.Product.GetByIdProduct;
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

            CreateMap<Product, GetAllProductsQueryResponse>()
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Product, GetByIdProductQueryResponse>().
                ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Product, CreateProductCommandRequest>().ReverseMap();
            CreateMap<Product, GetByCategoryProductsQueryResponse>().ReverseMap();



            CreateMap<Category, CreateCategoryCommandRequest>().ReverseMap();
            CreateMap<Category, GetAllCategoryQueryResponse>().ReverseMap();


            CreateMap<AppUser, CreateUserCommandRequest>().ReverseMap();
            CreateMap<AppUser, CreateUserDto>().ReverseMap();

            CreateMap<CreateUserCommandRequest, CreateUserDto>().ReverseMap();


        }
    }

}