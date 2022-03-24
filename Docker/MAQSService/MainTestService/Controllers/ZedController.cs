using MainTestService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace MainTestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZedController : ControllerBase
    {

        [AcceptVerbs("ZED")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Zed()
        {
            using StreamReader reader = new(Request.Body, Encoding.UTF8);
            var value = await reader.ReadToEndAsync();


            if(string.IsNullOrEmpty(value) || value.EndsWith('?'))
            {
                return StatusCode((int)HttpStatusCode.UseProxy, value);
            }

            return Ok($"\"{value}\"");
        }
    }
}