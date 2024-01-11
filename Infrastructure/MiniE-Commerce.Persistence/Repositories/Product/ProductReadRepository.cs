using System;
using Entities = MiniE_Commerce.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commorce.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using MiniE_Commerce.Persistence.Context;

namespace MiniE_Commerce.Persistence.Repositories.Product
{
    public class ProductReadRepository : ReadRepository<Entities.Product>, IProductReadRepository
    {
        private readonly AppDbContext _context;
        public ProductReadRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
