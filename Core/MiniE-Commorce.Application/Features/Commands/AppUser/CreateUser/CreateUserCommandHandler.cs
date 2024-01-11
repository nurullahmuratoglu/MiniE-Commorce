using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniE_Commorce.Application.Dtos.User;
using MiniE_Commorce.Application.Exceptions;
using MiniE_Commorce.Application.Interfaces.Services;
using Entities = MiniE_Commerce.Domain.Entities.Identity;

namespace MiniE_Commorce.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            var userDto = _mapper.Map<CreateUserDto>(request);
            var userResponseDto= await _userService.CreateAsync(userDto);
            return new() { Succeeded = userResponseDto.Succeeded, Message = userResponseDto.Message };
        }
    }
}
