using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.Category
{
    public class CreateCategoryCommandRequest:IRequest<Unit>
    {
        public string Name { get; set; }
        public int? ParrentCategoryId { get; set; } 
    }
}
