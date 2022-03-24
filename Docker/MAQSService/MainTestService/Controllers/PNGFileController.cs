using Microsoft.AspNetCore.Mvc;
using SkiaSharp;

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
                "RED" => SKColors.Red,
                "BLUE" => SKColors.Blue,
                "YELLOW" => SKColors.Yellow,
                "GREEN" => SKColors.Green,
                "BLACK" => SKColors.Black,
                "WHITE" => SKColors.White,
                "GRAY" or "GREY" => SKColors.Gray,
                _ => throw new NotSupportedException($"The color '{image}' is not available, please use a more common color."),
            };

            SKBitmap sKBitmap = new(200, 200);
            SKCanvas canvas = new(sKBitmap);
            canvas.Clear(color);

            return File(sKBitmap.Encode(SKEncodedImageFormat.Png, 100).ToArray(), "image/png");
        }
    }
}