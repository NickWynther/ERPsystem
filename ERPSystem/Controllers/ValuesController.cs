using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERPSystem.Controllers
{   
    //For Authorization Test purposes
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [Authorize]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<ValuesController>/auth
        [HttpGet("auth")]
        public string Auth()
        {
            if (User.Identity.IsAuthenticated)
            {
                return "Authenticated";
            }
            else
            {
                return "Guest";
            }
        }

        // GET api/<ValuesController>/role
        [HttpGet("role")]
        public string Role()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("User"))
                {
                    return "User";
                }
                else if (User.IsInRole("Admin"))
                {
                    return "Admin";
                }
                else
                    return "UnkownRole";
            }
            else
            {
                return "Guest";
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
