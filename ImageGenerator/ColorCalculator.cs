using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageGenerator
{
    public class ColorCalculator
    {
        private readonly ColorSpaceConverter _converter;
        
        public ColorCalculator()
        {
            _converter = new ColorSpaceConverter();
        }
        
        public Rgba32 GetTextColor(Rgba32 background)
        {
            var hsl = _converter.ToHsl(background);
            return _converter.ToRgb(new Hsl(hsl.H, hsl.S, 
                hsl.L switch
            {
                < 0.4F => hsl.L + 0.2F,
                >= 0.8F =>  hsl.L - 0.2F,
                _ => hsl.L - 0.2F
            }));
        }
    }
}