using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Dtos.Basket;
using MiniE_Commorce.Application.Interfaces.Repositories.Basket;
using MiniE_Commorce.Application.Interfaces.Repositories.BasketItem;
using MiniE_Commorce.Application.Interfaces.Services;
using MiniE_Commorce.Application.Interfaces.UnitOfWorks;
using System.Security.Claims;

namespace MiniE_Commerce.Persistence.Services
{


    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BasketService(IHttpContextAccessor contextAccessor, IBasketReadRepository baskeReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IUnitOfWork unitOfWork)
        {
            _contextAccessor = contextAccessor;
            _basketReadRepository = baskeReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddItemToBasketAsync(AddBasketItemDto basketItem)
        {

            var userId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var basket = await _basketReadRepository.GetAsync(predicate: x => x.UserId == userId, include: x => x.Include(b => b.BasketItems).ThenInclude(I => I.Product));

            if (basket == null)
            {
                Basket createBasket = new() { UserId = userId };

                createBasket.BasketItems = new List<BasketItem>() { new() { ProductId = basketItem.ProductId, Quantity = basketItem.Quantity } };


                await _basketWriteRepository.AddAsync(createBasket);
                await _unitOfWork.SaveAsync();
                return;
            }
            var hasBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == basketItem.ProductId);
            if (hasBasketItem == null)
            {

                await _basketItemWriteRepository.AddAsync(new() { ProductId = basketItem.ProductId, Quantity = basketItem.Quantity, BasketId = basket.Id });
                await _unitOfWork.SaveAsync();

                return;
            }
            hasBasketItem.Quantity = StockControl(hasBasketItem.Product.Stock, hasBasketItem.Quantity + basketItem.Quantity);
            await _basketItemWriteRepository.UpdateAsync(hasBasketItem);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Basket> GetBasketItemsAsync()
        {
            var userId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var basket = await _basketReadRepository.GetAsync(
                     predicate: x => x.UserId == userId,
                      include: x => x.Include(b => b.BasketItems).ThenInclude(bi => bi.Product), true);
            return basket;
        }

        public async Task RemoveBasketItemAsync(int basketItemId)
        {
            var basketItem = await _basketItemReadRepository.GetAsync(predicate: x => x.Id == basketItemId);
            await _basketItemWriteRepository.HardDeleteAsync(basketItem);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateQuantityAsync(UpdateBasketItemDto updateBasketItem)
        {
            var basketItem = await _basketItemReadRepository.GetAsync(predicate: x => x.Id == updateBasketItem.BasketItemId, include: x => x.Include(b => b.Product));

            basketItem.Quantity = StockControl(basketItem.Product.Stock, updateBasketItem.Quantity);
            await _basketItemWriteRepository.UpdateAsync(basketItem);
            await _unitOfWork.SaveAsync();

        }

        private int StockControl(int stock, int newQuantity)
        {
            return Math.Min(stock, newQuantity);
        }


    }
}
