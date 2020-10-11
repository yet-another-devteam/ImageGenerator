using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = SixLabors.ImageSharp.Color;
using PointF = SixLabors.ImageSharp.PointF;
using SizeF = SixLabors.ImageSharp.SizeF;

namespace ImageGenerator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageGeneratorController : ControllerBase
    {
        private readonly ColorCalculator _colorCalculator;
        private readonly FontFamily _fontFamily;

        public ImageGeneratorController(ColorCalculator colorCalculator, FontFamily fontFamily)
        {
            _colorCalculator = colorCalculator;
            _fontFamily = fontFamily;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([Range(1, 1024)] int width, [Range(1, 1024)] int height, [Required] string color, string caption)
        {
            if (!Rgba32.TryParseHex(color, out Rgba32 rgba))
                return BadRequest("Cannot parse color");
            
            var image = new Image<Rgba32>(width, height);
            image.Mutate(ctx => ctx.Fill(rgba));

            if (!string.IsNullOrWhiteSpace(caption))
            {
                const float fontSize = 75;
                
                Font font = _fontFamily.CreateFont(fontSize);
                TextGraphicsOptions textOptions = new()
                {
                    TextOptions = new()
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                    }
                };
            
                float textX = width / 2F;
                float textY = 7;
            
                image.Mutate(x => x.DrawText(textOptions, caption, font, _colorCalculator.GetTextColor(rgba), new PointF(textX, textY)));

            }

            var stream = new MemoryStream();
            await image.SaveAsJpegAsync(stream, HttpContext.RequestAborted);

            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "image/jpeg");
        }
    }
}