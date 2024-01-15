using MiniE_Commerce.Domain.Common;
using MiniE_Commerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Domain.Entities
{
    public class Basket:BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
