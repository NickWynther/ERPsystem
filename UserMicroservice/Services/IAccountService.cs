using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Enums;

namespace UserMicroservice.Services
{
    /// <summary>
    /// Service interface for login and registration.  
    /// </summary>
    public interface IAccountService
    {
        Task<RegisterDto> Register(RegisterDto model , RoleType role);
        Task<LoginResult> Login(LoginDto model);
    }
}
