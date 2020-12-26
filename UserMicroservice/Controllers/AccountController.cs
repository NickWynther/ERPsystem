using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Entities;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto model)
        { 
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser == null)
            {
                var registrator = new Registrator(_userManager);
                if (registrator.Register(model, "User").Result)
                {
                    return Created("", model);
                }
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser == null)
            {
                var registrator = new Registrator(_userManager);
                if (registrator.Register(model, "Admin").Result)
                {
                    return Created("", model);
                }
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
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
                    
                    return Ok(jwt);
                }
            }
            else
            {
                return Unauthorized();
            }
            
            return BadRequest();
        }
    }
}
