﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Dtos.Basket
{
    public class AddBasketItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
