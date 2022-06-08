using System;
using System.IO;
using System.Text;

namespace ShoppingList0806
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Aada\source\repos\AWACADEMY\KoulutusWeek1\ShoppingList0806\MyShopList.txt";
            bool addItems = true;
            while (addItems)
            {
                if (File.Exists(path))
                {
                    Console.Clear();
                    Console.WriteLine("The shopping list already has the following items");
                    ReadPrintText(path);
                    string userInput = AskingForInput(out addItems);
                    File.AppendAllText(path, userInput + Environment.NewLine);
                }
                else
                {
                    string userInput = AskingForInput(out addItems);
                    CreateAndWriteFile(path, userInput);
                }

                Console.WriteLine("Your shopping list looks like this now! Press any key to continue");
                ReadPrintText(path);
                Console.ReadKey();
            }


        }

        private static string AskingForInput(out bool addItems)
        {
            Console.WriteLine("Please give me a shopping item, enter q to end");
            string userInput = Console.ReadLine();
            if (userInput == "q")
            {
                addItems = false;
                return "";
            }
            else
            {
                addItems = true;
                return userInput;
            }
            

        }
        private static void ReadPrintText(string path)
        {
            string[] readText = File.ReadAllLines(path);
            int i;
            for (i = 0; i < readText.Length; i++)
            {
                Console.WriteLine(readText[i]);
            }
        }

        private static void CreateAndWriteFile(string path, string userInput)
        {
            using (FileStream fs = File.Create(path))
            {
                byte[] input = new UTF8Encoding(true).GetBytes(userInput);
                fs.Write(input, 0, input.Length);
            }
        }
    }
}
