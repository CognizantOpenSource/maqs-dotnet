using MainTestService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace AutomationTestSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XML_JSONController : ControllerBase
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };


        // GET /api/XML_JSON/GetAllProducts
        /// <summary>
        /// Gets all the products
        /// </summary>
        /// <returns>A string of all products<see cref="Product"/></returns>
        [HttpGet("GetAllProducts")]
        public ActionResult<Product[]> GetAllProducts()
        {
            if (products.Length < 1)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("GetProduct/{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            if (id > 1)
            {
                // Check the header for the security override first, only return a conflict if they don't have the override
                if (!Request.Headers.TryGetValue("pass", out StringValues values) || !values.First().Equals("word"))
                {
                    return Unauthorized("You're user does not have rights to delete this item");
                }
            }

            return Ok();
        }

        [HttpPut("Put/{id}")]
        public ActionResult Put(int id, Product? value)
        {
            if (value == null)
            {
                return Conflict($"No product provided for id = {id} ");
            }

            if (id > 20)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred attempting to update Servers");
            }

            return Ok();
        }

        [HttpPost("Post")]
        public IActionResult Post(Product value)
        {
            if (value == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Value is required");
            }

            if (value.Id < 4)
            {
                return Conflict("Duplicate value");
            }

            return Ok();
        }

        [HttpPatch("Patch/{id}")]
        public IActionResult Patch(int id, Product value)
        {
            if (value == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Value is required");
            }

            if (id > 20)
            {
                return NotFound($"No Product found for id = {id}");
            }

            return Ok(value);
        }
    }
}