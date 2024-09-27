using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _key = configuration["JwtSettings:SecretKey"];
            _issuer = configuration["JwtSettings:Issuer"];
            _audience = configuration["JwtSettings:Audience"];
        }

        public string GenerateToken(User user)
        {
            if (string.IsNullOrEmpty(_key))
                throw new ArgumentNullException(nameof(_key), "Secret key is not provided.");

            if (string.IsNullOrEmpty(_issuer))
                throw new ArgumentNullException(nameof(_issuer), "Issuer is not provided.");

            if (string.IsNullOrEmpty(_audience))
                throw new ArgumentNullException(nameof(_audience), "Audience is not provided.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
