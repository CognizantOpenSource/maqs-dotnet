using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AutomationTestSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PNGFileController : ControllerBase
    {
        // GET: api/PNGFile/GetImage
        /// <summary>
        /// Gets the image
        /// </summary>
        /// <param name="imageName">Color to be used</param>
        /// <returns>PNG image</returns>
        [HttpGet("GetImage")]
        public IActionResult GetImage(string? image = "red")
        {
            if (image == null)
            {
                return NotFound();
            }

            var color = image.ToUpper() switch
            {
                "RED" => "red",
                "BLUE" => "blue",
                "YELLOW" => "yellow",
                "GREEN" => "green",
                "BLACK" => "black",
                "WHITE" => "white",
                "GRAY" or "GREY" => "gray",
                _ => throw new NotSupportedException($"The color '{image}' is not available, please use a more common color."),
            };

            Assembly assembly = Assembly.GetExecutingAssembly();
            string fileName = $"{assembly.GetName().Name}.Static.{color}.png";

            return File(assembly.GetManifestResourceStream(fileName), "image/png");
        }
    }
}