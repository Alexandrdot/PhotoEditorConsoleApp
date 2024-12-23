using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public interface IFilter
{
    void Apply(Image image);
}

public class Filter1 : IFilter
{
    public void Apply(Image image)
    {
        image.Mutate(x => x.Brightness(1.5f)); // Осветление
    }
}

public class Filter2 : IFilter
{
    public void Apply(Image image)
    {
        image.Mutate(x => x.Hue(45f)); // Изменение оттенка
    }
}