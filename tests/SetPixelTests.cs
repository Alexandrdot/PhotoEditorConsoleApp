namespace tests;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
[TestClass]
public class SetPixelTests
{
    [TestMethod]
    public void ChangePixelColor_ValidCoordinates_ShouldChangePixelColor()
    {
        var image = new Image<Rgba32>(100, 100);
        var initialColor = image[50, 50];
        var newColor = new Rgba32(255, 0, 0); // Красный цвет

        SetPixel.ChangePixelColor(image, 50, 50, newColor);

        var changedColor = image[50, 50];
        Assert.AreEqual(newColor, changedColor);
        Assert.AreNotEqual(initialColor, changedColor);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void ChangePixelColor_InvalidCoordinates_ShouldThrowException()
    {
        var image = new Image<Rgba32>(100, 100);
        var invalidX = 200;
        var invalidY = 200;

        SetPixel.ChangePixelColor(image, invalidX, invalidY, new Rgba32(255, 0, 0)); // Ожидается исключение
    }
}
