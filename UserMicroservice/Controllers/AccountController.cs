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
using UserMicroservice.Data.Enums;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers
{
    [Route(Router.controller)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController( IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        ///  Create new user 
        /// </summary>
        [HttpPost(Router.registerUser)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto model) =>
            await _accountService.Register(model , RoleType.User) == null ? (IActionResult)BadRequest() : Created("", model);
        
        /// <summary>
        /// Create new admin
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost(Router.registerAdmin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto model) =>
             await _accountService.Register(model , RoleType.Admin) == null ? (IActionResult) BadRequest() : Created("", model);

        /// <summary>
        /// Log-in
        /// </summary>
        /// <returns>JWT token</returns>
        [HttpPost(Router.login)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _accountService.Login(model);
            if (result.Jwt != null)
            {
                return Ok(result.Jwt);
            }
            else
            {
                 return result.UserExist ? (IActionResult)BadRequest() : Unauthorized();
            } 
        }
    }
}
