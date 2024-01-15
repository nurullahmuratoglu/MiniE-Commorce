using MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Services
{
    public interface IBasketService
    {
        public Task<Basket> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(AddBasketItemDto basketItem);
        public Task UpdateQuantityAsync(UpdateBasketItemDto updateBasketItem);
        public Task RemoveBasketItemAsync(int basketItemId);
        
    }
}
