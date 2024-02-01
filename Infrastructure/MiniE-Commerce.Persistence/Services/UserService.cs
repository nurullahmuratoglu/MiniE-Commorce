using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using MiniE_Commerce.Domain.Entities.Identity;
using MiniE_Commorce.Application.Dtos.User;
using MiniE_Commorce.Application.Exceptions;
using MiniE_Commorce.Application.Interfaces.Services;
using MiniE_Commorce.Application.Interfaces.Services.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Persistence.Services
{
    public class UserService : IUserService
    {

        private readonly IRabbitMQService _rabbitMQService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _usermanager;

        public UserService(IMapper mapper, UserManager<AppUser> usermanager, IRabbitMQService rabbitMQService)
        {
            _mapper = mapper;
            _usermanager = usermanager;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto user)
        {
            var createUser = _mapper.Map<AppUser>(user);
            createUser.Id = Guid.NewGuid().ToString();
            IdentityResult result = await _usermanager.CreateAsync(createUser, user.Password);

            if (result.Succeeded)
            {
                _rabbitMQService.SendMeesageToExchange("UserFanoutExchange", createUser.Email);
                return new() { Succeeded = true, Message = "kullanıcı başarıyla oluşturulmuştur",UserId= createUser.Id };

            }
            throw new UserCreateFailedException(result.Errors);
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user,DateTime accessTokenDate,int addOnAccessTokenDate)
        {

            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _usermanager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }
    }
}
