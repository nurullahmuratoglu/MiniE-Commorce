using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandRequest:IRequest<Unit>
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
    }
}