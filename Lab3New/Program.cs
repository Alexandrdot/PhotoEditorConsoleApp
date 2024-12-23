using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main()
    {
        PhotoEditor editor = null;
        do
        {
            if (editor == null)
            {
                
                Console.WriteLine("Введите путь к изображению для загрузки:");
                string path = Console.ReadLine();


                if (File.Exists(path)) //проверка на наличие файла
                {
                    editor = new PhotoEditor();
                    editor.LoadPhoto(path);
                }
                else
                {
                    Console.WriteLine("Ошибка: Указанный файл не найден.");
                    continue;
                }
            }
            if (editor != null)
            {
                Console.WriteLine("Выберите операцию:");
                Console.WriteLine("1. Повернуть изображение");
                Console.WriteLine("2. Обрезать изображение");
                Console.WriteLine("3. Применить фильтр");
                Console.WriteLine("4. Составить коллаж");
                Console.WriteLine("5. Изменить пиксель");
                Console.WriteLine("6. Добавить текст");
                Console.WriteLine("7. Сохранить изображение");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    

                    case "2":
                        int imageWidth = editor.Photos[0].Width;
                        int imageHeight = editor.Photos[0].Height;
                        Console.WriteLine("Введите координаты (x, y)(точка отсчета) и размер (width, height) для обрезки:");
                        Console.WriteLine($"Текущие значения: ширина {imageWidth}, высота {imageHeight}");

                        Console.Write("x: ");
                        int.TryParse(Console.ReadLine(), out int x);
                        if (x > imageWidth || x < 0)
                        {
                            Console.WriteLine("Значение не входит в допустимые, взято по умолчанию х = 0");
                            x = 0;
                        }
                        Console.Write("y: ");
                        int.TryParse(Console.ReadLine(), out int y);
                        if (y > imageHeight || y < 0)
                        {
                            Console.WriteLine("Значение не входит в допустимые, взято по умолчанию y = 0");
                            y = 0;
                        }
                        Console.WriteLine($"Текущие значения: ширина {imageWidth-x}, высота {imageHeight-y}");
                        Console.Write("width: ");
                        int.TryParse(Console.ReadLine(), out int width);
                        if (width > imageWidth - x || width <= 0)
                        {
                            Console.WriteLine($"Некорректное значение, взято значение по умолчанию: {imageWidth - x}");
                            width = imageWidth - x;
                        }
                        Console.Write("height: ");
                        int.TryParse(Console.ReadLine(), out int height);
                        if (height > imageHeight - y || height <= 0)
                        {
                            Console.WriteLine($"Некорректное значение, взято значение по умолчанию: {imageHeight - y}");
                            height = imageHeight - y;
                        }
                        editor.CropPhoto(0, x, y, width, height);
                        Console.WriteLine("Изображение обрезано.");
                        break;

                    case "4":
                        Console.WriteLine("Создание коллажа из нескольких изображений");
                        List<Image> collagePhotos = new List<Image>
                        {
                            editor.Photos[0]
                        };
                        Console.WriteLine("Введите количество изображений:");
                        int.TryParse(Console.ReadLine(), out int count);
                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine($"Введите путь к изображению {i + 1}:");
                            string collagePath = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(collagePath))
                            {
                                Console.WriteLine("Нет такого пути.");
                                continue;
                            }
                            else if (File.Exists(collagePath)) //проверка на наличие файла
                            {
                                editor.LoadPhoto(collagePath);
                                collagePhotos.Add(editor.Photos[i+1]);
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: Указанный файл не найден.");
                            }
                            
                        }
                        var collage = Collage.CreateCollage(collagePhotos);
                        Console.WriteLine("введите полный путь для сохранения коллажа");
                        string collagepath = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(collagepath))
                        {
                            Console.WriteLine("Нет такого пути.");
                            break;
                        }
                        string fullPath = Path.GetFullPath(collagepath);
                        string directoryPath = Path.GetDirectoryName(fullPath);
                        string fileName = Path.GetFileName(fullPath);

                        if (!Directory.Exists(directoryPath))
                        {
                            Console.WriteLine($"Ошибка: Каталог '{directoryPath}' не существует.");
                            break;
                        }
                        if (File.Exists(fullPath))
                        {
                            Console.WriteLine($"Ошибка: Файл '{fileName}' уже существует.");
                            break;
                        }
                        collage.Save(collagepath);
                        Console.WriteLine("Коллаж сохранен");
                        break;

                    case "5":
                        Console.WriteLine("Введите координаты пикселя (x, y) и новый цвет (R, G, B):");
                        int imageWidthpx = editor.Photos[0].Width;
                        int imageHeightpy = editor.Photos[0].Height;
                        Console.WriteLine($"Текущие значения: ширина {imageWidthpx}, высота {imageHeightpy}");

                        Console.Write("px: ");
                        int.TryParse(Console.ReadLine(), out int px);
                        if (px > imageWidthpx || px < 0)
                        {
                            Console.WriteLine("Значение не входит в допустимые, взято по умолчанию pх = 0");
                            px = 0;
                        }
                        Console.Write("py: ");
                        int.TryParse(Console.ReadLine(), out int py);
                        if (py > imageHeightpy || py < 0)
                        {
                            Console.WriteLine("Значение не входит в допустимые, взято по умолчанию py = 0");
                            py = 0;
                        }
                        Console.Write("r: ");
                        int.TryParse(Console.ReadLine(), out int r);
                        if (r < 0 || r > 255)
                        {
                            Console.WriteLine("Некорректное значение, взято по умолчанию r = 0");
                            r = 0;
                        }
                        Console.Write("g: ");
                        int.TryParse(Console.ReadLine(), out int g);
                        if (g < 0 || g > 255)
                        {
                            Console.WriteLine("Некорректное значение, взято по умолчанию g = 0");
                            g = 0;
                        }
                        Console.Write("b: ");
                        int.TryParse(Console.ReadLine(), out int b);
                        if (b < 0 || b > 255)
                        {
                            Console.WriteLine("Некорректное значение, взято по умолчанию b = 0");
                            b = 0;
                        }
                        SetPixel.ChangePixelColor((Image<Rgba32>)editor.Photos[0], px, py, new Rgba32(r, g, b));
                        Console.WriteLine("Пиксель изменен");
                        break;

                    case "6":
                        Console.WriteLine("Введите текст, который хотите добавить на изображение:");
                        string text = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(text))
                            text = " ";

                        int imageWidthtext = editor.Photos[0].Width;
                        int imageHeighttext = editor.Photos[0].Height;
                        Console.WriteLine($"Текущие значения: ширина {imageWidthtext}, высота {imageHeighttext}");
                        Console.WriteLine("Введите координаты (x, y) для размещения текста:");
                        Console.Write("x: ");
                        int.TryParse(Console.ReadLine(), out int xText);
                        if(xText < 0 || xText > imageWidthtext)
                        {
                            Console.WriteLine($"Некорректное значение, по умолчанию: {imageWidthtext/2}");
                            xText = imageWidthtext;
                        }
                        Console.Write("y: ");
                        int.TryParse(Console.ReadLine(), out int yText);
                        if (yText < 0 || yText > imageHeighttext)
                        {
                            Console.WriteLine($"Некорректное значение, по умолчанию: {imageHeighttext / 2}");
                            yText = imageHeighttext / 2;
                        }

                        Console.Write("Введите размер шрифта:");
                        float.TryParse(Console.ReadLine(), out float fontSize);
                        if(fontSize <= 0)
                        {
                            Console.WriteLine("Недопустимое значение. По умолчанию: 17");
                            fontSize = 15;
                        }

                        Console.WriteLine("Введите цвет текста (R, G, B):");
                        Console.Write("r: ");
                        int.TryParse(Console.ReadLine(), out int r1);
                        if (r1 < 0 || r1 > 255)
                        {
                            Console.WriteLine("Некорректное значение, взято по умолчанию r = 0");
                            r1 = 0;
                        }
                        Console.Write("g: ");
                        int.TryParse(Console.ReadLine(), out int g1);
                        if (g1 < 0 || g1 > 255)
                        {
                            Console.WriteLine("Некорректное значение, взято по умолчанию g = 0");
                            g1 = 0;
                        }
                        Console.Write("b: ");
                        int.TryParse(Console.ReadLine(), out int b1);
                        if (b1 < 0 || b1 > 255)
                        {
                            Console.WriteLine("Некорректное значение, взято по умолчанию b = 0");
                            b1 = 0;
                        }

                        editor.AddTextToPhoto((Image<Rgba32>)editor.Photos[0], text, xText, yText, fontSize, new Rgba32((byte)r1, (byte)g1, (byte)b1));
                        Console.WriteLine("Текст добавлен на изображение.");

                        break;

                    case "7":
                        Console.WriteLine("Введите полный путь для сохранения изображения:");
                        string savePath = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(savePath))
                        {
                            Console.WriteLine("Нет такого пути.");
                            break;
                        }
                        string _fullPath = Path.GetFullPath(savePath);
                        string _directoryPath = Path.GetDirectoryName(_fullPath);
                        string _fileName = Path.GetFileName(_fullPath);
                        if (Directory.Exists(_directoryPath))
                        {
                            try
                            {
                                editor.SavePhoto(0, savePath);
                                editor = null; // Обнуляем editor только после успешного сохранения
                                Console.WriteLine("Изображение сохранено");
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка при сохранении изображения: {ex.Message}");
                            }
                        }
                        else if (File.Exists(_fullPath))
                        {
                            Console.WriteLine($"Ошибка: Файл '{_fileName}' уже существует.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Указанный каталог не существует.");
                        }
                        
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор");
                        break;
                }
            }
        } while (Console.ReadKey().Key != ConsoleKey.Escape); //реализовал выход по esc
    }
}