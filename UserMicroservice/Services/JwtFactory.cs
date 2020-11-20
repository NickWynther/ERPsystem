using Microsoft.AspNetCore.Identity;
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
    public class JwtFactory
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

        public JwtDto Create(User user , int minutes)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            var userRoles = _userManager.GetRolesAsync(user).Result;
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _issuer,
               _audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: credentials
                );

            return new JwtDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        } 
    }
}
