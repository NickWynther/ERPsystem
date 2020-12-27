using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Entities;

namespace UserMicroservice.Services
{
    public class JwtFactory : IJwtFactory 
    {
        private readonly UserManager<User> _userManager;
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        public JwtFactory(UserManager<User> userManager , string key , string issuer , string audience)
        {
            _userManager = userManager;
            _key = key;
            _issuer = issuer;
            _audience = audience;
        }

        public JwtFactory(UserManager<User>  userManager , IConfiguration config)
        {
            _userManager = userManager;
            _key = config["Tokens:Key"];
            _issuer = config["Tokens:Issuer"];
            _audience = config["Tokens:Audience"];
        }

        public Jwt Create(User user , int minutes)
        {
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var claims = CreateClaims(user, userRoles);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _issuer,
               _audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: credentials
                );

            return new Jwt
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Roles = userRoles.ToList()
            };
        }

        private static List<Claim> CreateClaims(User user, IList<string> userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            userRoles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
