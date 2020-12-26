using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Controllers
{
    public class Router
    {
        public const string controller = "api/[controller]";
        public const string registerUser = "register/user";
        public const string registerAdmin = "register/admin";
        public const string login = "login";
    }
}
