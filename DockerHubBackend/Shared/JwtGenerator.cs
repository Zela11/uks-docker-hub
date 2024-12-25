using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DockerHubBackend.DTO;
using DockerHubBackend.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Marketing_system.DA.Contracts.Shared
{
    public class JwtGenerator : ITokenGeneratorRepository
    {
        private readonly string _key = Environment.GetEnvironmentVariable("JWT_KEY") ?? "uks-docker-hub-application_secret_key";
        private readonly string _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "uks-docker";
        private readonly string _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "uks-docker-front.com";

        public async Task<TokenDTO> GenerateToken(int id, string email, string type)
        {
            var authenticationResponse = new TokenDTO();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("id", id.ToString()),
                new("email", email),
                new(ClaimTypes.Role, type)
            };

            var jwt = CreateToken(claims, 60 * 24);
            authenticationResponse.Id = id;
            authenticationResponse.AccessToken = jwt;

            return authenticationResponse;
        }

        /*public Result<AuthenticationTokensDto> GeneratePasswordResetToken(User user)
        {
            var authenticationResponse = new AuthenticationTokensDto();

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("email", user.Email),
            new(ClaimTypes.Role, user.GetPrimaryRoleName())
        };

            var jwt = CreateToken(claims, 10);
            authenticationResponse.Id = user.Id;
            authenticationResponse.AccessToken = jwt;

            return authenticationResponse;
        }*/

        private string CreateToken(IEnumerable<Claim> claims, double expirationTimeInMinutes)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
