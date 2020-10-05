using System.IO;
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
        public IActionResult Get([FromQuery(Name = "width")] int width, [FromQuery(Name = "height")] int height, [FromQuery(Name = "color")] string color)
        {
            if (color == null)
                return BadRequest();
            
            if(!Rgba32.TryParseHex(color, out Rgba32 rgba))
                return BadRequest("Cannot parse color");
            
            var image = new Image<Rgba32>(width, height);
            image.Mutate(ctx => ctx.Fill(rgba));

            var stream = new MemoryStream();
            image.SaveAsJpeg(stream);

            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "image/jpeg");
        }
    }
}