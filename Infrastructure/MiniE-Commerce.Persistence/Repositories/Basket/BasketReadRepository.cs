using System;
using System.Collections.Generic;
using Entites = MiniE_Commerce.Domain.Entities;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commorce.Application.Interfaces.Repositories.Basket;
using Microsoft.EntityFrameworkCore;
using MiniE_Commerce.Persistence.Context;

namespace MiniE_Commerce.Persistence.Repositories.Basket
{
    public class BasketReadRepository : ReadRepository<Entites.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
