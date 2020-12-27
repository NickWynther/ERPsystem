using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Data.Dto
{
    /// <summary>
    /// Jwt object contains token directly and also additional usefull unecoded information for client
    /// </summary>
    public class Jwt
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> Roles { get; set; }

    }
}
