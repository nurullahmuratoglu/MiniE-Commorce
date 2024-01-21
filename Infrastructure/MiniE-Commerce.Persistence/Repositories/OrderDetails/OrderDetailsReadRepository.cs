using System;
using System.Collections.Generic;
using Entities = MiniE_Commerce.Domain.Entities;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commorce.Application.Interfaces.Repositories.OrderDetails;
using MiniE_Commerce.Persistence.Context;

namespace MiniE_Commerce.Persistence.Repositories.OrderDetails
{
    public class OrderDetailsReadRepository : ReadRepository<Entities.OrderDetails>, IOrderDetailsReadRepository
    {
        public OrderDetailsReadRepository(AppDbContext dbContext) : base(dbContext)
        {


        }
    }
}
