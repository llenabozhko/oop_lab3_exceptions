using System;
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
                        Console.ReadLine();
                        break;
                    case 2:
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
    }
}