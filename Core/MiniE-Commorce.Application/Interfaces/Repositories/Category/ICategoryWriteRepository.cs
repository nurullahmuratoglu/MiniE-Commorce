using System;
using Entities = MiniE_Commerce.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Repositories.Category
{
    public interface ICategoryWriteRepository: IWriteRepository<Entities.Category>
    {
    }
}
