using MainTestService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MainTestService.Controllers
{
    [ApiController]
    public class FileAPIController : ControllerBase
    {
        [HttpPost, Route("api/upload")]
        public IActionResult Index()
        {
            var request = HttpContext.Request;

            if (!request.HasFormContentType)
            {
                return StatusCode((int)HttpStatusCode.UnsupportedMediaType, "Content is not 'multipart/form-data'");
            }

            FilesUploaded name = new();

            foreach (var file in request.Form.Files)
            {
                name.Files.Add(new Files()
                {
                    ContentName = file.Name,
                    FileName = file.FileName,
                    DateUploaded = DateTime.Now
                });
            }

            return Ok(name);
        }
    }
}