using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class AuthenticateUserCommand : IRequest<UserDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Device { get; set; }
        public string IpAddress { get; set; }
        public string Browser {  get; set; }
    }
}
