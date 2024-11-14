using System;
using System.IO;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.IO.Compression;
using System.Collections.Generic;

class Program
{
    static string fileDirectory = @"C:\Users\Irina\source\repos\1_prak";

    static void Main(string[] args)
    {
        // Убедимся, что директория существует, если нет — создадим
        if (!Directory.Exists(fileDirectory))
        {
            Directory.CreateDirectory(fileDirectory);
        }

        MainMenu();
    }

    // Главное меню
    static void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\nГлавное меню:");
            Console.WriteLine("1. Работа с файлами");
            Console.WriteLine("2. Работа с JSON");
            Console.WriteLine("3. Работа с XML");
            Console.WriteLine("4. Работа с архивами");
            Console.WriteLine("5. Информация о дисках");
            Console.WriteLine("0. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    FileMenu();
                    break;
                case "2":
                    JsonMenu();
                    break;
                case "3":
                    XmlMenu();
                    break;
                case "4":
                    ArchiveMenu();
                    break;
                case "5":
                    DisplayDiskInfo();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова.");
                    break;
            }
        }
    }

    // Вывод информации о дисках
    static void DisplayDiskInfo()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();

        Console.WriteLine("\nИнформация о дисках:");
        foreach (DriveInfo drive in drives)
        {
            Console.WriteLine($"Название диска: {drive.Name}");
            if (drive.IsReady)
            {
                Console.WriteLine($"Тип диска: {drive.DriveType}");
                Console.WriteLine($"Файловая система: {drive.DriveFormat}");
                Console.WriteLine($"Общий размер: {drive.TotalSize} байт");
                Console.WriteLine($"Свободное место: {drive.TotalFreeSpace} байт");
                Console.WriteLine($"Доступное свободное место: {drive.AvailableFreeSpace} байт");
            }
            else
            {
                Console.WriteLine("Диск не готов.");
            }
            Console.WriteLine();
        }
    }

    // Меню работы с файлами
    static void FileMenu()
    {
        while (true)
        {
            Console.WriteLine("\nМеню работы с файлами:");
            Console.WriteLine("1. Создать файл");
            Console.WriteLine("2. Записать строку в файл");
            Console.WriteLine("3. Прочитать файл");
            Console.WriteLine("4. Удалить файл");
            Console.WriteLine("0. Назад");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateFile();
                    break;
                case "2":
                    WriteToFile();
                    break;
                case "3":
                    ReadFile();
                    break;
                case "4":
                    DeleteFile();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова.");
                    break;
            }
        }
    }

    static void CreateFile()
    {
        Console.WriteLine("Введите имя файла для создания:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);
        try
        {
            using (FileStream fs = File.Create(filepath))
            {
                Console.WriteLine($"Файл {filepath} создан.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при создании файла: {e.Message}");
        }
    }

    static void WriteToFile()
    {
        Console.WriteLine("Введите имя файла для записи:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);

        Console.WriteLine("Введите строку для записи:");
        string content = Console.ReadLine();

        try
        {
            File.AppendAllText(filepath, content + Environment.NewLine);
            Console.WriteLine($"Строка записана в файл {filepath}.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при записи в файл: {e.Message}");
        }
    }

    static void ReadFile()
    {
        Console.WriteLine("Введите имя файла для чтения:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);

        try
        {
            string content = File.ReadAllText(filepath);
            Console.WriteLine($"Содержимое файла {filepath}:\n{content}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filepath} не найден.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при чтении файла: {e.Message}");
        }
    }

    static void DeleteFile()
    {
        Console.WriteLine("Введите имя файла для удаления:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);

        try
        {
            File.Delete(filepath);
            Console.WriteLine($"Файл {filepath} удалён.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filepath} не найден.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при удалении файла: {e.Message}");
        }
    }

    // Меню работы с JSON
    static void JsonMenu()
    {
        while (true)
        {
            Console.WriteLine("\nМеню работы с JSON:");
            Console.WriteLine("1. Создать JSON файл");
            Console.WriteLine("2. Добавить ключ:значение в JSON файл");
            Console.WriteLine("3. Прочитать JSON файл");
            Console.WriteLine("4. Удалить JSON файл");
            Console.WriteLine("0. Назад");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateJsonFile();
                    break;
                case "2":
                    AddKeyValueToJson();
                    break;
                case "3":
                    ReadJsonFile();
                    break;
                case "4":
                    DeleteFile();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова.");
                    break;
            }
        }
    }

    static void CreateJsonFile()
    {
        Console.WriteLine("Введите имя JSON файла для создания:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);

        Console.WriteLine("Введите ключ:");
        string key = Console.ReadLine();

        Console.WriteLine("Введите значение:");
        string value = Console.ReadLine();

        var jsonData = new { Key = key, Value = value };

        try
        {
            string jsonString = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, jsonString);
            Console.WriteLine($"Файл {filepath} создан.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при создании JSON файла: {e.Message}");
        }
    }

    static void AddKeyValueToJson()
    {
        Console.WriteLine("Введите имя JSON файла:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);

        Console.WriteLine("Введите ключ:");
        string key = Console.ReadLine();

        Console.WriteLine("Введите значение:");
        string value = Console.ReadLine();

        try
        {
            var jsonData = File.Exists(filepath) ? JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(filepath)) : new Dictionary<string, string>();
            jsonData[key] = value;

            string jsonString = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, jsonString);
            Console.WriteLine($"Ключ: {key}, значение: {value} добавлены в файл.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при добавлении данных в JSON файл: {e.Message}");
        }
    }

    static void ReadJsonFile()
    {
        Console.WriteLine("Введите имя JSON файла для чтения:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);

        try
        {
            string jsonString = File.ReadAllText(filepath);
            Console.WriteLine($"Содержимое файла:\n{jsonString}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filepath} не найден.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при чтении JSON файла: {e.Message}");
        }
    }

    // Меню работы с XML
    static void XmlMenu()
    {
        while (true)
        {
            Console.WriteLine("\nМеню работы с XML:");
            Console.WriteLine("1. Создать XML файл");
            Console.WriteLine("2. Добавить элемент в XML файл");
            Console.WriteLine("3. Прочитать XML файл");
            Console.WriteLine("4. Удалить XML файл");
            Console.WriteLine("0. Назад");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateXmlFile();
                    break;
                case "2":
                    AddElementToXml();
                    break;
                case "3":
                    ReadXmlFile();
                    break;
                case "4":
                    DeleteFile();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова.");
                    break;
            }
        }
    }

    static void CreateXmlFile()
    {
        Console.WriteLine("Введите имя XML файла:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);

        Console.WriteLine("Введите имя корневого элемента:");
        string rootElement = Console.ReadLine();

        var doc = new XDocument(new XElement(rootElement));

        try
        {
            doc.Save(filepath);
            Console.WriteLine($"Файл {filepath} создан.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при создании XML файла: {e.Message}");
        }
    }

    static void AddElementToXml()
    {
        Console.WriteLine("Введите имя XML файла:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);

        Console.WriteLine("Введите имя элемента:");
        string elementName = Console.ReadLine();

        Console.WriteLine("Введите текст элемента:");
        string elementText = Console.ReadLine();

        try
        {
            var doc = XDocument.Load(filepath);
            doc.Root.Add(new XElement(elementName, elementText));
            doc.Save(filepath);
            Console.WriteLine($"Элемент {elementName} добавлен в файл.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filepath} не найден.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при добавлении элемента: {e.Message}");
        }
    }

    static void ReadXmlFile()
    {
        Console.WriteLine("Введите имя XML файла для чтения:");
        string filename = Console.ReadLine();
        string filepath = Path.Combine(fileDirectory, filename);

        try
        {
            var doc = XDocument.Load(filepath);
            Console.WriteLine($"Содержимое файла:\n{doc}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filepath} не найден.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при чтении XML файла: {e.Message}");
        }
    }

    // Меню работы с архивами
    static void ArchiveMenu()
    {
        while (true)
        {
            Console.WriteLine("\nМеню работы с архивами:");
            Console.WriteLine("1. Создать архив");
            Console.WriteLine("2. Добавить файл в архив");
            Console.WriteLine("3. Разархивировать файл");
            Console.WriteLine("4. Удалить архив");
            Console.WriteLine("0. Назад");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateZipArchive();
                    break;
                case "2":
                    AddFileToZip();
                    break;
                case "3":
                    ExtractZipArchive();
                    break;
                case "4":
                    DeleteFile();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова.");
                    break;
            }
        }
    }

    static void CreateZipArchive()
    {
        Console.WriteLine("Введите имя архива (с расширением .zip):");
        string archiveName = Console.ReadLine();
        string archivePath = Path.Combine(fileDirectory, archiveName);

        try
        {
            using (var archive = ZipFile.Open(archivePath, ZipArchiveMode.Create))
            {
                Console.WriteLine($"Архив {archiveName} создан.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при создании архива: {e.Message}");
        }
    }

    static void AddFileToZip()
    {
        Console.WriteLine("Введите имя архива (с расширением .zip):");
        string archiveName = Console.ReadLine();
        string archivePath = Path.Combine(fileDirectory, archiveName);

        Console.WriteLine("Введите имя файла для добавления:");
        string fileName = Console.ReadLine();
        string filePath = Path.Combine(fileDirectory, fileName);

        try
        {
            using (var archive = ZipFile.Open(archivePath, ZipArchiveMode.Update))
            {
                archive.CreateEntryFromFile(filePath, fileName);
                Console.WriteLine($"Файл {fileName} добавлен в архив {archiveName}.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filePath} не найден.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при добавлении файла в архив: {e.Message}");
        }
    }

    static void ExtractZipArchive()
    {
        Console.WriteLine("Введите имя архива для разархивирования (с расширением .zip):");
        string archiveName = Console.ReadLine();
        string archivePath = Path.Combine(fileDirectory, archiveName);

        string extractPath = Path.Combine(fileDirectory, "extracted");

        try
        {
            ZipFile.ExtractToDirectory(archivePath, extractPath);
            Console.WriteLine($"Архив {archiveName} распакован в папку {extractPath}.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Архив {archiveName} не найден.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при разархивировании: {e.Message}");
        }
    }
}