using MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Interfaces.Repositories;
using MiniE_Commorce.Application.Interfaces.Services.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Infrastructure.Services.Redis
{
    public class CategoryHashSetService : RedisHashSetService<Category>, ICategoryCacheService
    {
        public CategoryHashSetService(string hashSetName, IConnectionMultiplexer redisCon, IReadRepository<Category> readRepository) : base(hashSetName, redisCon, readRepository)
        {
        }
    }
}
