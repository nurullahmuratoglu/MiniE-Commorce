using MiniE_Commerce.Domain.Common;
using MiniE_Commerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Domain.Entities
{
    public class Order:BaseEntity
    {


        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public float TotalPrice { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }



    }
}
