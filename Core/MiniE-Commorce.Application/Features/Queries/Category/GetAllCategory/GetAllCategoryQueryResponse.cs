﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Queries.Category.GetAllCategory
{
    public class GetAllCategoryQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetAllCategoryQueryResponse> Subcategories { get; set; }
    }
}
