using MiniE_Commerce.Persistence.Context;
using MiniE_Commorce.Application.Interfaces.Repositories.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = MiniE_Commerce.Domain.Entities;


namespace MiniE_Commerce.Persistence.Repositories.Order
{
    public class OrderWriteRepository:WriteRepository<Entities.Order>,IOrderWriteRepository
    {
        public OrderWriteRepository(AppDbContext appDbContext):base(appDbContext)
        {
            
        }
    }
}
