using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SWP.FUGoodsExchangeManagement.Business.Service.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AuthenticationService(IConfiguration config)
        {
            _config = config;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public (string accessToken, string refreshToken) GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:JwtKey"]));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new()
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                new Claim("userId", user.Id),
                new Claim("email", user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential
                );

            var encodedToken = _tokenHandler.WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();

            return (encodedToken, refreshToken);
        }

        public string decodeToken(string jwtToken, string nameClaim)
        {
            Claim? claim = _tokenHandler.ReadJwtToken(jwtToken).Claims.FirstOrDefault(selector => selector.Type.Equals(nameClaim));
            return claim != null ? claim.Value : "Error!!!";
        }
    }
}
