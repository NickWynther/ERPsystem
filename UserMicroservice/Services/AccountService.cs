using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Entities;
using UserMicroservice.Data.Enums;

namespace UserMicroservice.Services
{
    /// <summary>
    /// Service for login and registration.  
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;

        public AccountService( SignInManager<User> signInManager, UserManager<User> userManager, 
            IJwtFactory jwtFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
        }

        private async Task<bool> UserExist(string email) 
            => await _userManager.FindByEmailAsync(email) != null;
        
        public async Task<RegisterDto> Register(RegisterDto model , RoleType role)
        {
            if (!await UserExist(model.Email))
            {
                User user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                IdentityResult result = _userManager.CreateAsync(user, model.Password).Result;

                if (!result.Succeeded)
                {
                    return null;
                }
                else 
                {
                    await _userManager.AddToRoleAsync(user, role.ToString());
                }

                return model;
            }

            return null;
        }

        public async Task<LoginResult> Login(LoginDto model)
        {
            var result = new LoginResult() 
            { 
                UserExist = UserExist(model.Email).Result
            };

            if (result.UserExist)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.PasswordCheck = passwordCheck.Succeeded)
                {
                    result.Jwt = _jwtFactory.Create(user, 60);
                }
            }

            return result;
        }
    }
}
