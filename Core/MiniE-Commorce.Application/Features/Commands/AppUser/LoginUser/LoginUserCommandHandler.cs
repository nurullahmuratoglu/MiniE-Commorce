using MediatR;
using Entities = MiniE_Commerce.Domain.Entities.Identity;

using MediatR.Pipeline;
using MiniE_Commorce.Application.Features.Queries.Product.GetByCategoryProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MiniE_Commorce.Application.Exceptions;
using MiniE_Commorce.Application.Interfaces.Token;
using MiniE_Commorce.Application.Interfaces.Services;

namespace MiniE_Commorce.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {

        private readonly  IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var tokendto = await _authService.LoginAsync(request.UsernameOrEmail,request.Password,10000);
            return new() { Token = tokendto };

        }
    }
}
