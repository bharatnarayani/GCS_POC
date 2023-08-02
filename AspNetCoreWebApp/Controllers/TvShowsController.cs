using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreWebApp.Models;
using AspNetCoreWebApp.CloudStorage;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreWebApp.Controllers
{
    [Route("api/TVShows")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        private readonly ICloudStorage _cloudStorage;
        public TvShowsController(ICloudStorage cloudStorage)
        {
            _cloudStorage = cloudStorage;
        }

        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            return Ok("Hello");
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(IFormFile file)
        {
            var url = await UploadFile(file);
            return Ok(url);
        }
        private async Task<string> UploadFile(IFormFile file)
        {
            string fileNameForStorage = FormFileName("myTitle", file.FileName);
            var url = await _cloudStorage.UploadFileAsync(file, fileNameForStorage);
            return url;
        }
        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }
    }
}
