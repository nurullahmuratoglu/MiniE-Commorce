using MiniE_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Services.Redis
{
    public interface ICacheService<T> where T : class, IBaseEntity, new()
    {
        Task<T> GetAsync(string key);
        Task SetAsync(string key, T data);
        Task RemoveAsync(string key);
        Task<IEnumerable<T>> GetAllAsync();

    }
}
