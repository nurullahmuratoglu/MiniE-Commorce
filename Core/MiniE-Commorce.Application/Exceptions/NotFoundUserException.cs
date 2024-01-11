using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException(string? message) : base(message)
        {
        }
    }
}
