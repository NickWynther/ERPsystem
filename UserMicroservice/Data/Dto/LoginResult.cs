using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Data.Dto
{
    public class LoginResult
    {
        public JwtDto Jwt { get; set; }
        public bool UserExist { get; set; }
        public bool PasswordCheck { get; set; }
    }
}
