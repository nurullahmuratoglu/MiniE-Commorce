using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniE_Commerce.Domain.Entities.Identity;
using MiniE_Commerce.Persistence.Context;
using MiniE_Commerce.Persistence.Repositories;
using MiniE_Commerce.Persistence.Repositories.Basket;
using MiniE_Commerce.Persistence.Repositories.BasketItem;
using MiniE_Commerce.Persistence.Repositories.Category;
using MiniE_Commerce.Persistence.Repositories.Product;
using MiniE_Commerce.Persistence.Services;
using MiniE_Commerce.Persistence.UnitOfWorks;
using MiniE_Commorce.Application.Interfaces.Repositories;
using MiniE_Commorce.Application.Interfaces.Repositories.Basket;
using MiniE_Commorce.Application.Interfaces.Repositories.BasketItem;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using MiniE_Commorce.Application.Interfaces.Services;
using MiniE_Commorce.Application.Interfaces.UnitOfWorks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Persistence
{
    public static class Registration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SQLConnection"));
            }, ServiceLifetime.Scoped);

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager<SignInManager<AppUser>>();



            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();



            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();




            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();


            services.AddScoped<IBasketService, BasketService>();


            
        }
    }
}
