using MiniE_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Domain.Entities
{
    public class OrderDetails:BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalProductPrice { get; set; }//price*quantity


    }
}
