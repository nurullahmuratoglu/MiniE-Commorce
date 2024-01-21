using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Queries.Product.GetByCategoryProducts
{
    public class GetByCategoryProductsQueryResponse
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
