using MiniE_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product(string name, int stock, float price)
        {
            Name = name;
            Stock = stock;
            Price = price;
        }
        public Product()
        {
            
        }
    }
}
