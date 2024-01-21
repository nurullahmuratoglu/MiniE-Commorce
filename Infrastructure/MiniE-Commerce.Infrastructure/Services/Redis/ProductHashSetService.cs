using MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Interfaces.Repositories;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using MiniE_Commorce.Application.Interfaces.Services.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniE_Commerce.Infrastructure.Services.Redis
{
    public class ProductHashSetService : RedisHashSetService<Product>, IProductCacheService
    {
        public ProductHashSetService(string hashSetName, IConnectionMultiplexer redisCon, IReadRepository<Product> readRepository) : base(hashSetName, redisCon, readRepository)
        {
        }
    }
}
