using System;
using System.Collections.Generic;
using Entities = MiniE_Commerce.Domain.Entities;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commorce.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commerce.Persistence.Context;

namespace MiniE_Commerce.Persistence.Repositories.Category
{
    public class CategoryWriteRepository : WriteRepository<Entities.Category>, ICategoryWriteRepository
    {
        private readonly AppDbContext _context;
        public CategoryWriteRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
