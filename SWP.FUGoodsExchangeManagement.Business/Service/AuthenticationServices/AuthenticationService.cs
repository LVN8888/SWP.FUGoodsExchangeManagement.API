using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public string GenerateJWT(User User)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:JwtKey"]));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new()
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role),
                new Claim("userId", User.Id),
                new Claim("email", User.Email),
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential
                );
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        public string decodeToken(string jwtToken, string nameClaim)
        {
            Claim? claim = _tokenHandler.ReadJwtToken(jwtToken).Claims.FirstOrDefault(selector => selector.Type.ToString().Equals(nameClaim));
            return claim != null ? claim.Value : "Error!!!";
        }
    }
}
