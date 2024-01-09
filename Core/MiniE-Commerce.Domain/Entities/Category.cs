using MiniE_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Domain.Entities
{
    public class Category : BaseEntity
    {

        public string Name { get; set; }
        public int Priorty { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category(string name, int priorty)
        {

            Name = name;
            Priorty = priorty;
        }
        public Category()
        {

        }
    }
}
