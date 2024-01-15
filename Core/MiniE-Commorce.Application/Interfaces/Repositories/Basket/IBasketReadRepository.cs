using System;
using Entites = MiniE_Commerce.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commerce.Domain.Entities;

namespace MiniE_Commorce.Application.Interfaces.Repositories.Basket
{
    public interface IBasketReadRepository:IReadRepository<Entites.Basket>
    {
    }
}
