using MiniE_Commorce.Application.Dtos.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        public TokenDto Token { get; set; }
    }
}
