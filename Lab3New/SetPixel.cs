using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public class SetPixel
{
    public static void ChangePixelColor(Image<Rgba32> image, int x, int y, Rgba32 color)
    {
        image[x, y] = color;
    }
}