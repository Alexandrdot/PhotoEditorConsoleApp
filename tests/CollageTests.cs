using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace tests;
[TestClass]
public class CollageTests
{
    [TestMethod]
    public void CreateCollage_ValidPhotos_ShouldCreateCollage()
    {
        var image1 = new Image<Rgba32>(100, 100);
        var image2 = new Image<Rgba32>(150, 100);

        var collage = Collage.CreateCollage(new List<Image> { image1, image2 });

        Assert.AreEqual(250, collage.Width); // 100 + 150
        Assert.AreEqual(100, collage.Height); // Ожидаемая максимальная высота
    }
}