﻿using System;
using System.Collections.Generic;
using System.Linq;
using Entities = MiniE_Commerce.Domain.Entities;

using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Repositories.OrderDetails
{
    public interface IOrderDetailsWriteRepository : IWriteRepository<Entities.OrderDetails>
    {
    }
}
