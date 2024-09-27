using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, decimal>
    {
        private readonly IUserRepository _userRepository;
        public GetBalanceQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<decimal> Handle(GetBalanceQuery query, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(query.Token);
            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            return user.Balance;
        }
    }
}
