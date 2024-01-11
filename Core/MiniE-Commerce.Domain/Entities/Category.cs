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
        //public int Priorty { get; set; }
        public int? ParrentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public List<Category> Subcategories { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; }
        public Category(string name, int parrentCategoryId)
        {
            ParrentCategoryId = parrentCategoryId;
            Name = name;

        }
        public Category()
        {

        }
    }
}
