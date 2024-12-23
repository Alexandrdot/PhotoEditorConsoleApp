using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace tests;

[TestClass]
public class FiltersTests
{
    [TestMethod]
    public void Filter1_Apply_ShouldChangeBrightness()
    {
        string path = "test_image.jpg";

        // Создаем изображение (например, серый квадрат)
        var image = new Image<Rgba32>(100, 100, new Rgba32(128, 128, 128)); // Средний серый цвет
        image.Save(path);

        var filter = new Filter1();

        // Измеряем начальную яркость пикселя
        var initialColor = image[50, 50]; // Пиксель в центре изображения
        var initialBrightness = GetBrightness(initialColor);

        // Применяем фильтр
        filter.Apply(image);

        // Измеряем новую яркость
        var newColor = image[50, 50];
        var newBrightness = GetBrightness(newColor);

        // Выводим начальную и новую яркость для отладки
        Console.WriteLine($"Initial Brightness: {initialBrightness}");
        Console.WriteLine($"New Brightness: {newBrightness}");

        // Проверяем, что яркость изменилась (ожидаем увеличение)
        Assert.IsTrue(newBrightness > initialBrightness, "Яркость не увеличилась");

        // Удаляем файл после теста
        File.Delete(path);
    }

    private float GetBrightness(Rgba32 color)
    {
        // Формула для вычисления яркости
        return 0.2126f * color.R + 0.7152f * color.G + 0.0722f * color.B;
    }




    [TestMethod]
    public void Filter2_Apply_ShouldChangeHue()
    {
        string path = "test_image.jpg";
        // Создаём изображение (например, красный квадрат)
        var image = new Image<Rgba32>(100, 100, Color.Red);
        image.Save(path);

        var filter = new Filter2();
        var initialColor = image[50, 50]; // Сохраняем начальный цвет пикселя (красный)

        filter.Apply(image); // Применение фильтра

        var newColor = image[50, 50]; // Пиксель на той же позиции после фильтра

        // Проверяем, что цвет изменился (ожидаем, что оттенок изменится)
        Assert.AreNotEqual(initialColor, newColor);

        File.Delete(path); // Удаляем файл после теста
    }
}
