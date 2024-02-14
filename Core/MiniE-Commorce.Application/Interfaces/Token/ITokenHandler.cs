using MiniE_Commerce.Domain.Entities.Identity;
using MiniE_Commorce.Application.Dtos.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Token
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(AppUser appUser);
        string CreateRefreshToken(); 
    }
}
