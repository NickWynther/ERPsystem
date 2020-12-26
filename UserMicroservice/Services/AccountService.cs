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
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AccountService( SignInManager<User> signInManager, UserManager<User> userManager, 
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        public async Task<RegisterDto> Register(RegisterDto model , RoleType role)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser == null)
            {
                var registrator = new Registrator(_userManager);
                if (registrator.Register(model, role.ToString()).Result)
                {
                    return model;
                }
            }

            return null;
        }

        public async Task<LoginResult> Login(LoginDto model)
        {  
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (passwordCheck.Succeeded)
                {
                    var jwt = new JwtFactory(
                        _userManager,
                        _config["Tokens:Key"],
                        _config["Tokens:Issuer"],
                        _config["Tokens:Audience"]
                        ).Create(user, 60);

                    return new LoginResult() { Jwt = jwt, UserExist = true, PasswordCheck = true };
                }
            }
            else
            {
                return new LoginResult() { Jwt = null, UserExist = false , PasswordCheck = false };
            }

            return new LoginResult() { Jwt = null, UserExist = true, PasswordCheck = false };
        }
    }
}
