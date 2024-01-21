using MiniE_Commorce.Application.Interfaces.Repositories.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities = MiniE_Commerce.Domain.Entities;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commerce.Persistence.Context;
using System.Buffers.Text;

namespace MiniE_Commerce.Persistence.Repositories.OrderDetails
{
    public class OrderDetailsWriteRepository: WriteRepository<Entities.OrderDetails>, IOrderDetailsWriteRepository
    {
        public OrderDetailsWriteRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }
    }
}
