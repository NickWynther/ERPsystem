using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Entities;

namespace UserMicroservice.Services
{
    public class Registrator
    {
        private readonly UserManager<User> _userManager;
        public Registrator(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Register(RegisterDto model, string role)
        {
            User user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            IdentityResult result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return result.Succeeded;
        }
        

    }
}
