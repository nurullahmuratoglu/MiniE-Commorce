using MiniE_Commorce.Application.Interfaces.Services.Authhenrications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {
        
    }
}
