using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public class PhotoEditor
{
    public List<Image> Photos { get; set; }

    public PhotoEditor()
    {
        Photos = new List<Image>();
    }

    public void LoadPhoto(string path)
    {
        var photo = Image.Load(path);
        Photos.Add(photo);
    }

    public void SavePhoto(int index, string path)
    {
        if (index >= 0 && index < Photos.Count)
        {
            Photos[index].Save(path);
        }
    }

    public void RotatePhoto(int index, float degrees)
    {
        if (index >= 0 && index < Photos.Count)
        {
            Photos[index].Mutate(x => x.Rotate(degrees));
        }
    }

    public void CropPhoto(int index, int x, int y, int width, int height)
    {
        if (index >= 0 && index < Photos.Count)
        {
            Photos[index].Mutate(ctx => ctx.Crop(new Rectangle(x, y, width, height)));
        }
    }

    public void ApplyFilter(int index, IFilter filter)
    {
        if (index >= 0 && index < Photos.Count)
        {
            filter.Apply(Photos[index]);
        }
    }
    public void AddTextToPhoto(Image<Rgba32> image, string text, int x, int y, float fontSize, Rgba32 color)
    {
        if (image == null)
        {
            throw new ArgumentNullException(nameof(image), "Изображение не может быть null.");
        }

        try
        {
            
            FontFamily fontFamily = SystemFonts.Families.FirstOrDefault(); 
            var font = fontFamily.CreateFont(fontSize);
            
            // Рисование текста на изображении
            image.Mutate(ctx => ctx.DrawText(text, font, color, new PointF(x, y)));
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при добавлении текста к изображению: {ex.Message}", ex);
        }
    }
}
