﻿using System;
using System.Collections.Generic;
using Entities = MiniE_Commerce.Domain.Entities;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Repositories.OrderDetails
{
    public interface IOrderDetailsReadRepository:IReadRepository<Entities.OrderDetails>
    {
    }
}
