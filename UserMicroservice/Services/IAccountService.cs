using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Enums;

namespace UserMicroservice.Services
{
    public interface IAccountService
    {
        public Task<RegisterDto> Register(RegisterDto model , RoleType role);
        public Task<LoginResult> Login(LoginDto model);
    }
}
