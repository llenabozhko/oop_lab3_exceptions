using System;

namespace oop_lab3_exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("1. Filereader");
                Console.WriteLine("2. Image Reflector");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("block1");
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Exit");
                        break;

                }
            } while (choice != 0);
        }
    }
}