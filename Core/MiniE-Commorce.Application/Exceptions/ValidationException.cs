using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Exceptions
{
    public class ValidationException:Exception
    {
        public ValidationException(List<string> messages):base(string.Join(",", messages))
        {

        }
    }
}
