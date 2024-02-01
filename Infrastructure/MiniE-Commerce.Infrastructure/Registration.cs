using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniE_Commerce.Infrastructure.BackgroundService;
using MiniE_Commerce.Infrastructure.Services.Email;
using MiniE_Commerce.Infrastructure.Services.RabbitMQ;
using MiniE_Commerce.Infrastructure.Services.Redis;
using MiniE_Commerce.Infrastructure.Services.Token;
using MiniE_Commorce.Application.BaseAppSettings;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using MiniE_Commorce.Application.Interfaces.Services.Email;
using MiniE_Commorce.Application.Interfaces.Services.RabbitMQ;
using MiniE_Commorce.Application.Interfaces.Services.Redis;
using MiniE_Commorce.Application.Interfaces.Token;
using RabbitMQ.Client;
using StackExchange.Redis;

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

            services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQURL")) });

            services.AddSingleton<IRabbitMQService,RabbitMQService>();
            services.AddHostedService<UserRegistrationEmailService>();

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddSingleton<IEmailService, EmailService>();
        }
    }
}
