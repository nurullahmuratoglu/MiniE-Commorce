using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniE_Commerce.Domain.Entities;
using MiniE_Commorce.Application.Dtos.Basket;
using MiniE_Commorce.Application.Interfaces.Repositories.Basket;
using MiniE_Commorce.Application.Interfaces.Repositories.Order;
using MiniE_Commorce.Application.Interfaces.Repositories.OrderDetails;
using MiniE_Commorce.Application.Interfaces.Services;
using MiniE_Commorce.Application.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderDetailsReadRepository _orderDetailsReadRepository;
        private readonly IOrderDetailsWriteRepository _orderDetailsWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketWriteRepository _basketWriteRepository;



        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, IOrderDetailsReadRepository orderDetailsReadRepository, IOrderDetailsWriteRepository orderDetailsWriteRepository, IHttpContextAccessor httpContextAccessor, IBasketService basketService, IMapper mapper, IUnitOfWork unitOfWork, IBasketWriteRepository basketWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _orderDetailsReadRepository = orderDetailsReadRepository;
            _orderDetailsWriteRepository = orderDetailsWriteRepository;
            _httpContextAccessor = httpContextAccessor;
            _basketService = basketService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _basketWriteRepository = basketWriteRepository;
        }

        public async Task CreateOrder()
        {
            
            var basket =await _basketService.GetBasketItemsAsync();
            var order=_mapper.Map<Order>(basket);
            order.TotalPrice = CalculateTotalPrice(order.OrderDetails);
            order.OrderDetails.ForEach(x=>x.TotalProductPrice= x.Quantity * x.Product.Price);
            
            await _orderWriteRepository.AddAsync(order);
            await _basketWriteRepository.HardDeleteAsync(basket);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Order> GetOrder()
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = await _orderReadRepository.GetAsync(predicate:x=>x.UserId==userId,include:x=>x.Include(o=>o.OrderDetails).ThenInclude(x=>x.Product));
            return order;
        }
        private float CalculateTotalPrice(List<OrderDetails> orderDetails)
        {
            return orderDetails?.Sum(item => item.Quantity * item.Product.Price) ?? 0;
        }
    }
}
