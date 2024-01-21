using MiniE_Commerce.Domain.Common;
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
    public class RedisHashSetService<T> : ICacheService<T> where T : class, IBaseEntity, new()
    {
        private readonly IConnectionMultiplexer _redisCon;
        protected readonly IDatabase _database;
        protected readonly string _hashSetName;
        private readonly IReadRepository<T> _readRepository;


        public RedisHashSetService(string hashSetName, IConnectionMultiplexer redisCon, IReadRepository<T> readRepository)
        {
            _redisCon = redisCon;
            _database = redisCon.GetDatabase();
            _hashSetName = hashSetName;
            _readRepository = readRepository;
        }
        public async Task<T> GetAsync(string key)
        {
            var serializedData = await _database.HashGetAsync(_hashSetName, key);
            return serializedData.HasValue ? JsonSerializer.Deserialize<T>(serializedData) : null;
        }

        public async Task SetAsync(string key, T data)
        {
            var serializedData = JsonSerializer.Serialize(data);
            await _database.HashSetAsync(_hashSetName, key, serializedData);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.HashDeleteAsync(_hashSetName, key);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (!await _database.KeyExistsAsync(_hashSetName))
            {
                return await LoadToCacheFromDbAsync();
            }


            var hashEntries = await _database.HashGetAllAsync(_hashSetName);
            return hashEntries.Select(entry => JsonSerializer.Deserialize<T>(entry.Value));
        }
        private async Task<List<T>> LoadToCacheFromDbAsync()
        {
            var entities = await _readRepository.GetAllAsync();
            List<T> entityList = entities.ToList();

            var tasks = entityList.Select(async entity =>
            {
                await _database.HashSetAsync(_hashSetName, entity.Id, JsonSerializer.Serialize(entity));
            });

            await Task.WhenAll(tasks);

            return entityList;
        }
    }
}
