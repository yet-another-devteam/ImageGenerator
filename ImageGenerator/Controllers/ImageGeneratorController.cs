using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ImageGenerator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageGeneratorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([Range(1, 1024)] int width, [Range(1, 1024)] int height, [Required] string color)
        {
            if (!Rgba32.TryParseHex(color, out Rgba32 rgba))
                return BadRequest("Cannot parse color");
            
            var image = new Image<Rgba32>(width, height);
            image.Mutate(ctx => ctx.Fill(rgba));

            var stream = new MemoryStream();
            await image.SaveAsJpegAsync(stream, HttpContext.RequestAborted);

            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "image/jpeg");
        }
    }
}