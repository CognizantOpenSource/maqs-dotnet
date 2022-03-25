using MainTestService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace MainTestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringController : ControllerBase
    {
        readonly Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        [HttpGet("Get")]
        public string Get()
        {
            string toBeUsed = string.Empty;

            foreach (var p in products)
            {
                toBeUsed += BuildString(p);
            }

            return toBeUsed;
        }

        [HttpGet("{id}", Order = 0 )]
        public ActionResult<Product> GetProductString(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpGet("{name}", Order = 1 )]
        public ActionResult<Product> GetProductString(string name)
        {
            var product = products.FirstOrDefault((p) => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            using StreamReader reader = new(Request.Body);
            var value = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(value))
            {
                var message = new ReturnMessage
                {
                    Message = "Value is required"
                };

                return BadRequest(message);
            }
            else if (value.Contains("Product ID: 1"))
            {
                return Conflict("Duplicate value");
            }

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 3 || id < 0)
            {
                var message = new ReturnMessage
                {
                    Message = "Resource was not found"
                };

                return NotFound(message);
            }

            return Ok();
        }

        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id)
        {
            if (id > 20 || id < 1)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred attempting to update Servers");
            }

            using StreamReader reader = new(Request.Body, Encoding.UTF8);
            var value = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(value))
            {
                var message = new ReturnMessage
                {
                    Message = $"No product data provided for id = {id} "
                };

                return StatusCode((int)HttpStatusCode.Conflict, message);
            }

            return Ok();
        }

        [HttpPatch("Patch/{id}")]
        public async Task<IActionResult> Patch(int id)
        {            
            if (id > 20 || id < 1)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred attempting to update Servers");
            }

            using StreamReader reader = new(Request.Body, Encoding.UTF8);
            var value = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(value))
            {
                var message = new ReturnMessage
                {
                    Message = "Value is required"
                };

                return BadRequest(message);
            }

            return Ok("\"Patched\"");
        }

        private static string BuildString(Product product)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Product ID: " + product.Id + " ");
            sb.AppendLine("Product Name: " + product.Name + " ");
            sb.AppendLine("Product Category " + product.Category + " ");
            sb.AppendLine("Product Price " + product.Price + " ");

            return sb.ToString();
        }
    }
}