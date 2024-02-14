using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MiniE_Commerce.Infrastructure.BackgroundService;
using MiniE_Commerce.Infrastructure.Services.Email;
using MiniE_Commerce.Infrastructure.Services.RabbitMQ;
using MiniE_Commerce.Infrastructure.Services.Redis;
using Token= MiniE_Commerce.Infrastructure.Services.Token;
using MiniE_Commerce.Infrastructure.Settings;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using MiniE_Commorce.Application.Interfaces.Services.Email;
using MiniE_Commorce.Application.Interfaces.Services.RabbitMQ;
using MiniE_Commorce.Application.Interfaces.Services.Redis;
using MiniE_Commorce.Application.Interfaces.Token;
using RabbitMQ.Client;
using StackExchange.Redis;
using System.Security.Claims;
using System.Text;

namespace MiniE_Commerce.Infrastructure
{

    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.Configure<TokenSettings>(configuration.GetSection("Token"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            var tokenOptions = configuration.GetSection("Token").Get<TokenSettings>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin", options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true, //Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanıcı belirlediğimiz değerdir. -> www.bilmemne.com
                        ValidateIssuer = true, //Oluşturulacak token değerini kimin dağıttını ifade edeceğimiz alandır. -> www.myapi.com
                        ValidateLifetime = true, //Oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır.
                        ValidateIssuerSigningKey = true, //Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden suciry key verisinin doğrulanmasıdır.

                        ValidAudience = tokenOptions.Audience,
                        ValidIssuer = tokenOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                        NameClaimType = ClaimTypes.Name, //JWT üzerinde Name claimne karşılık gelen değeri User.Identity.Name propertysinden elde edebiliriz.

                    };
                });





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
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IRabbitMQService,RabbitMQService>();
            services.AddHostedService<UserRegistrationEmailService>();
            
           
            services.AddTransient<ITokenHandler, Token.TokenHandler>();
        }
    }
}
