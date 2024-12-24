using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
//using static System.Console;

namespace oop_lab3_exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int choice;

            do
            {
                Console.WriteLine("1. Files Reader");
                Console.WriteLine("2. Image Reflector");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Running Block1:");
                        DoTask1();
                        break;
                    case 2:
                        DoTask2();
                        break;
                    default:
                        Console.WriteLine("Exit");
                        break;

                }
            } while (choice != 0);
        }

        static void DoTask1()
        {
            string inputFiles = "../../../../input_files";
            List<string> noFile = new();
            List<string> badData = new();
            List<string> overflow = new();

            List<int> products = new List<int>();

            for (int i = 10; i <= 29; i++)
            {
                string file = Path.Combine(inputFiles, $"{i}.txt");

                try
                {
                    StreamReader sr = new StreamReader(file);
                    int first = int.Parse(sr.ReadLine());
                    int second = int.Parse(sr.ReadLine());
                    int product = checked(first * second);
                    products.Add(product);
                }
                catch (FileNotFoundException)
                {
                    noFile.Add($"{i}.txt");
                }
                catch (FormatException)
                {
                    badData.Add($"{i}.txt");
                }
                catch (OverflowException)
                {
                    overflow.Add($"{i}.txt");
                }
            }
            int result = products.Sum() / products.Count;
            Console.WriteLine($"Середнє значення: {result}");

            WriteListToFIle(noFile, "../../../../report_files/no_file.txt");
            WriteListToFIle(badData, "../../../../report_files/bad_data.txt");
            WriteListToFIle(overflow, "../../../../report_files/overflow.txt");

        }

        static void WriteListToFIle(List<string> list, string filePath)
        {
            try
            {
                File.WriteAllLines(filePath, list);
                Console.WriteLine($"Файл успішно записано за шляхом: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка при записі у файл: {ex.Message}");
            }
        }
        static void DoTask2()
        {
            Console.WriteLine("Обробка зображень у поточній папці з дзеркальним відображенням.\n");

            string currentDirectory = "../../../../";
                //+ Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(currentDirectory);
            Regex regexExtForImage = new Regex("^.((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$", RegexOptions.IgnoreCase);

            foreach (string file in files)
            {
                try
                {
                    if (!regexExtForImage.IsMatch(Path.GetExtension(file)))
                        throw new NotSupportedException($"Файл \"{file}\" пропущено: не є графічним.");

                    Console.WriteLine($"Обробляємо файл: {Path.GetFileName(file)}");

                    Bitmap bitmap;
                    try
                    {
                        bitmap = new Bitmap(file);
                    }
                    catch
                    {
                        throw new Exception($"Файл \"{file}\" не вдалося прочитати як зображення.");
                    }

                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    string newFileName = Path.Combine(currentDirectory,
                        $"{Path.GetFileNameWithoutExtension(file)}-mirrored.gif");

                    try
                    {
                        bitmap.Save(newFileName, ImageFormat.Gif);
                        Console.WriteLine($"Збережено як: {newFileName}");
                    }
                    catch
                    {
                        throw new IOException($"Помилка при збереженні файлу \"{newFileName}\".");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }

            Console.WriteLine("\nОбробка завершена.");
        }
    }
}