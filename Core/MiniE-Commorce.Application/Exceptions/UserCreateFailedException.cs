using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException(IEnumerable<IdentityError> errors) : base(GetAllErrorMessages(errors))
        {
        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        private static string GetAllErrorMessages(IEnumerable<IdentityError> errors)
        {
            return string.Join("; ", errors.Select(error => error.Description));
        }
    }
}
