using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniE_Commerce.Domain.Entities.Identity;
using MiniE_Commorce.Application.Dtos.Token;
using MiniE_Commorce.Application.Exceptions;
using MiniE_Commorce.Application.Features.Commands.AppUser.LoginUser;
using MiniE_Commorce.Application.Interfaces.Services;
using MiniE_Commorce.Application.Interfaces.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Persistence.Services
{
    public class AuthService : IAuthService
    {


        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;

        public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<TokenDto> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            var user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null) user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null) throw new NotFoundUserException("kullanıcı adı veya şifre yanlış");



            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {

                var tokendto= _tokenHandler.CreateAccessToken(1000, user);
                await _userService.UpdateRefreshTokenAsync(tokendto.RefreshToken, user, tokendto.Expiration, 15000);
                return tokendto;
                
            }
            throw new NotFoundUserException("kullanıcı adı veya şifre yanlış");
        }

        public async Task<TokenDto> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                TokenDto token = _tokenHandler.CreateAccessToken(10000, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15000);
                return token;
            }
            else
                throw new NotFoundUserException();
        }
    }
}
