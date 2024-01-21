using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniE_Commerce.Domain.Common;
using MiniE_Commerce.Infrastructure.Services.Redis;
using MiniE_Commerce.Infrastructure.Services.Token;
using MiniE_Commorce.Application.Interfaces.Repositories;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using MiniE_Commorce.Application.Interfaces.Services.Redis;
using MiniE_Commorce.Application.Interfaces.Token;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Infrastructure
{

    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            // Redis bağlantısı için ConnectionMultiplexer'ı yapılandırın

            var multiplexer = ConnectionMultiplexer.Connect(configuration["CacheOptions:Url"]);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddScoped(typeof(ICacheService<>), typeof(RedisHashSetService<>));

            
            services.AddScoped<IProductCacheService>(sp =>
            {
                var connectionMultiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
                var productReadRepository = sp.GetRequiredService<IProductReadRepository>();
                return new ProductHashSetService("Product", multiplexer, productReadRepository);
            });
            services.AddScoped<ICategoryCacheService>(sp =>
            {
                var connectionMultiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
                var categoryReadRepository = sp.GetRequiredService<ICategoryReadRepository>();
                return new CategoryHashSetService("Category", multiplexer, categoryReadRepository);
            });
        }
    }
}
