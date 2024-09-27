using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthenticateUserCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<UserDTO> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsernameAsync(command.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            if (user.IsFirstSignIn)
            {
                user.IsFirstSignIn = false;
                user.Balance += 5.0m;
                user.Device = command.Device;
                user.IpAddress = command.IpAddress;
                user.LastBrowser = command.Browser;
                await _userRepository.UpdateUserAsync(user);
            }
            else
            {
                user.Device = command.Device;
                user.IpAddress = command.IpAddress;
                user.LastBrowser = command.Browser;
                await _userRepository.UpdateUserAsync(user);
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };
        }
    }
}
