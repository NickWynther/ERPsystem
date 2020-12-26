using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Data.Dto;
using UserMicroservice.Data.Entities;

namespace UserMicroservice.Services
{
    public interface IJwtFactory
    {
        JwtDto Create(User user, int minutes);
    }
}
