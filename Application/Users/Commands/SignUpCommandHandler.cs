using Domain;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        public SignUpCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var user = new User()
            {
                CreatedAt = DateTime.UtcNow,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Password = hashedPassword,
                Username = command.UserName,
                Device = command.Device,
                IpAddress = command.IpAddress
            };
            await _userRepository.AddUserAsync(user);
            return Unit.Value;
        }
    }
}
