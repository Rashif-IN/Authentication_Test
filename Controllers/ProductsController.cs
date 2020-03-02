using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using authtest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authtest.Controllers
{
    [ApiController]
    [Route("product")]
    [Authorize]

    public class ProductsController : ControllerBase
    {

        private readonly Contextt konteks;

        public ProductsController(Contextt context)
        {
            konteks = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            List<object> allData = new List<object>();
            var data = konteks.Products;
            foreach (var x in data)
            {
                allData.Add(new { x.id, x.name, x.price});
            }
            return Ok(new { Message = "Success retreiving data", Status = true, Data = allData });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int ID)
        {
            var data = konteks.Products.Find(ID);

            if (data == null)
            {
                return NotFound(new { Message = "Customer not found", Status = false });
            }

            return Ok(new { Message = "Success retreiving data", Status = true, Data = data });
        }

        [HttpPost]
        public IActionResult Post(Products data)
        {
            konteks.Products.Add(data);
            konteks.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Products data)
        {
            var query = konteks.Products.Find(id);
            query.name = data.name;
            query.price = data.price;
            query.updated_at = DateTime.Now;
            konteks.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await konteks.Products.FindAsync(id);

            if (data == null)
            {
                return NotFound(new { Message = "Customer not found", Status = false });
            }

            konteks.Products.Remove(data);
            await konteks.SaveChangesAsync();

            return Ok();
        }
    }
}