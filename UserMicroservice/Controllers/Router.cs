using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Controllers
{
    /// <summary>
    /// This class contains constant paths for controller methods
    /// </summary>
    public class Router
    {
        public const string controller = "api/[controller]";
        public const string registerUser = "register/user";
        public const string registerAdmin = "register/admin";
        public const string login = "login";
    }
}
