using AutoMapper;
using MiniE_Commerce.Domain.Entities;
using MiniE_Commerce.Domain.Entities.Identity;
using MiniE_Commorce.Application.Dtos.Basket;
using MiniE_Commorce.Application.Dtos.Order;
using MiniE_Commorce.Application.Dtos.User;
using MiniE_Commorce.Application.Features.Commands.AppUser.CreateUser;
using MiniE_Commorce.Application.Features.Commands.BasketItem.AddBasketItem;
using MiniE_Commorce.Application.Features.Commands.BasketItem.UpdateBasketItem;
using MiniE_Commorce.Application.Features.Commands.Category;
using MiniE_Commorce.Application.Features.Commands.Product.CreateProduct;
using MiniE_Commorce.Application.Features.Queries.Basket.GetBasketItems;
using MiniE_Commorce.Application.Features.Queries.Category.GetAllCategory;
using MiniE_Commorce.Application.Features.Queries.Order;
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
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            CreateMap<Product, GetAllProductsQueryResponse>()
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Product, GetByIdProductQueryResponse>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();

            CreateMap<Product, CreateProductCommandRequest>().ReverseMap();
            CreateMap<Product, GetByCategoryProductsQueryResponse>().ReverseMap();



            CreateMap<Category, CreateCategoryCommandRequest>().ReverseMap();
            CreateMap<Category, GetAllCategoryQueryResponse>().ReverseMap();


            CreateMap<AppUser, CreateUserCommandRequest>().ReverseMap();
            CreateMap<AppUser, CreateUserDto>().ReverseMap();

            CreateMap<CreateUserCommandRequest, CreateUserDto>().ReverseMap();


            CreateMap<AddBasketItemCommandRequest, AddBasketItemDto>().ReverseMap();



            CreateMap<UpdateBasketItemCommandRequest, UpdateBasketItemDto>().ReverseMap();
            CreateMap<BasketItem, GetBasketItemDto>()
              .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
              .ForMember(dest => dest.BasketItemId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price));


            CreateMap<Basket, GetBasketItemsQueryResponse>().ReverseMap();



            CreateMap<Basket, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.BasketItems))
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();


            CreateMap<BasketItem, OrderDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<Order, GetOrderQueryResponse>()
                 .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                 .ReverseMap();



            CreateMap<OrderDetails, OrderDetailsDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ReverseMap();





        }
    }

}