using System;
using System.Collections.Generic;
using Entites = MiniE_Commerce.Domain.Entities;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commorce.Application.Interfaces.Repositories.BasketItem;
using Microsoft.EntityFrameworkCore;
using MiniE_Commerce.Persistence.Context;

namespace MiniE_Commerce.Persistence.Repositories.BasketItem
{
    public class BasketItemWriteRepository : WriteRepository<Entites.BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
