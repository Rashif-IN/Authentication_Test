
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using authtest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace authtest.Controllers
{
    [ApiController]
    [Route("customer")]
    [Authorize]

    public class CustomersController : ControllerBase
    {

        private readonly Contextt konteks;

        public CustomersController(Contextt context)
        {
            konteks = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            List<object> allData = new List<object>();
            var data = konteks.Customers;
            foreach (var x in data)
            {
                allData.Add(new { x.id, x.full_name, x.username, x.email, x.phone_number });
            }
            return Ok(new { Message = "Success retreiving data", Status = true, Data = allData });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int ID)
        {
            var data = konteks.Customers.Find(ID);

            if (data == null)
            {
                return NotFound(new { Message = "Customer not found", Status = false });
            }

            return Ok(new { Message = "Success retreiving data", Status = true, Data = data });
        }

        [HttpPost]
        public IActionResult Post(Customers data)
        {
            konteks.Customers.Add(data);
            konteks.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Customers data)
        {
            var query = konteks.Customers.Find(id);
            query.full_name = data.full_name;
            query.username = data.username;
            query.email = data.email;
            query.phone_number = data.phone_number;
            query.updated_at = DateTime.Now;
            konteks.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await konteks.Customers.FindAsync(id);

            if (data == null)
            {
                return NotFound(new { Message = "Customer not found", Status = false });
            }

            konteks.Customers.Remove(data);
            await konteks.SaveChangesAsync();

            return Ok();
        }


        //[HttpPost]
        //[AllowAnonymous]
        //[Route("authenticate")]
        //public IActionResult Authenticate(Customers customers)
        //{


        //    var _user = customers.Find(e => e.username == user.username);

        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var tokenDescription = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]{
        //            new Claim(ClaimTypes.Name, _user.username),
        //            new Claim(ClaimTypes.Country, "Indonesia")
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ini secret key nya harus panjang")), SecurityAlgorithms.HmacSha512Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescription);

        //    var tokenResponse = new
        //    {
        //        token = tokenHandler.WriteToken(token),
        //        user = _user.username
        //    };

        //    return Ok(tokenResponse);
        //}
    }


}
