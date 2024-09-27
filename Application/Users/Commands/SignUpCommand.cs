using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class SignUpCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Device { get; set; }
        public string IpAddress { get; set; }
    }
}
