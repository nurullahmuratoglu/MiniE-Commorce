using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email,string body,string subject=null);
    }
}
