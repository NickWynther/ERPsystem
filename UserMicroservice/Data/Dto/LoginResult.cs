using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Data.Dto
{
    /// <summary>
    /// AccountService.Login method result. Controlers send response codes based on result properties
    /// </summary>
    public class LoginResult
    {
        public Jwt Jwt { get; set; }
        public bool UserExist { get; set; } = false;
        public bool PasswordCheck { get; set; } = false;
    }
}
