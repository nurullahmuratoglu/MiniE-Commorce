using MiniE_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateOrder();
        Task<Order> GetOrder();




    }
}
