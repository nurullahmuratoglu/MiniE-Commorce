using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities = MiniE_Commerce.Domain.Entities;
using System.Threading.Tasks;
using MiniE_Commorce.Application.Interfaces.Repositories.Order;
using MiniE_Commerce.Persistence.Context;

namespace MiniE_Commerce.Persistence.Repositories.Order
{
    public class OrderReadRepository:ReadRepository<Entities.Order>,IOrderReadRepository
    {
        public OrderReadRepository(AppDbContext appDbContext):base(appDbContext)
        {
            
        }
    }
}
