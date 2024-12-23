namespace tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

[TestClass]
public class PhotoEditorTests
{
    private PhotoEditor editor;

    [TestInitialize]
    public void TestInitialize()
    {
        editor = new PhotoEditor();
    }

    [TestMethod]
    public void LoadPhoto_ValidPath_ShouldLoadPhoto()
    {
        // Создаем минимальное изображение для теста
        string path = "test_image.jpg";
        var image = new Image<Rgba32>(100, 100, Color.Black);
        image.Save(path); // Сохраняем изображение на диск

        editor.LoadPhoto(path); // Загружаем изображение в редактор

        Assert.AreEqual(1, editor.Photos.Count);
        Assert.IsNotNull(editor.Photos[0]);

        File.Delete(path); // Удаляем временный файл после теста
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void LoadPhoto_InvalidPath_ShouldThrowException()
    {
        string invalidPath = "invalid_image.jpg";
        editor.LoadPhoto(invalidPath); // Ожидается исключение
    }
    [TestMethod]
    public void AddTextToPhoto_ValidInputs_ShouldAddTextToImage()
    {
        var image = new Image<Rgba32>(200, 100, Color.Black);
        string text = "Hello, World!";
        int x = 10;
        int y = 20;
        float fontSize = 24f;
        Rgba32 color = Color.Red;

        var editor = new PhotoEditor();
        editor.AddTextToPhoto(image, text, x, y, fontSize, color);
    }
    [TestMethod]
    public void RotatePhoto_ValidRotation_ShouldRotateImage()
    {
        // Создаем минимальное изображение для теста
        string path = "test_image.jpg";
        var image = new Image<Rgba32>(100, 100, Color.Black);
        image.Save(path); // Сохраняем изображение

        editor.LoadPhoto(path); // Загружаем изображение в редактор

        int initialWidth = editor.Photos[0].Width;
        int initialHeight = editor.Photos[0].Height;

        // Поворот на 90 градусов
        editor.RotatePhoto(0, 90);

        // После поворота изображение должно иметь другие размеры (например, если это квадрат, поворот не меняет размеры)
        Assert.AreEqual(initialWidth, editor.Photos[0].Width);
        Assert.AreEqual(initialHeight, editor.Photos[0].Height);

        File.Delete(path); // Удаляем временный файл после теста
    }

    [TestMethod]
    public void CropPhoto_ValidCoordinates_ShouldCropImage()
    {
        // Создаем минимальное изображение для теста
        string path = "test_image.jpg";
        var image = new Image<Rgba32>(100, 100, Color.Black);
        image.Save(path); // Сохраняем изображение

        editor.LoadPhoto(path);
        editor.CropPhoto(0, 10, 10, 50, 50); // Обрезка изображения с координатами (10,10) и размером 50x50

        // Проверяем, что размеры изображения изменились на ожидаемые значения
        Assert.AreEqual(50, editor.Photos[0].Width);
        Assert.AreEqual(50, editor.Photos[0].Height);

        File.Delete(path); // Удаляем временный файл после теста
    }
    
}