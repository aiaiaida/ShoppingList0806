using System;
using System.IO;
using System.Text;

namespace ShoppingList0806
{
    class Program
    {
        static void Main(string[] args)
        {
            // initiate a path to create file or read file
            string path = @"C:\Users\Aada\source\repos\AWACADEMY\KoulutusWeek1\ShoppingList0806\MyShopList.txt";
            // bool value to control the main while loop
            // if user wants to continue to add new items or not
            bool addItems = true;
            while (addItems)
            {
                if (File.Exists(path))
                {
                    // if file exists, firstly return all items to user
                    // then ask for input and use append method
                    Console.Clear();
                    Console.WriteLine("The shopping list already has the following items");
                    ReadPrintText(path);
                    string userInput = AskingForInput(out addItems);
                    File.AppendAllText(path, userInput + Environment.NewLine);
                }
                // if file doesn't exit, ask for user input
                // create a new file and write into it 
                else
                {
                    string userInput = AskingForInput(out addItems);
                    CreateAndWriteFile(path, userInput);
                }

                // return all the items to user after input
                Console.WriteLine("Your shopping list looks like this now!");
                Console.WriteLine(" Press any key to continue.");
                ReadPrintText(path);
                Console.ReadKey();
            }


        }

        // a method to ask for input and return false to bool value if user enters q
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

        // read the file and write all the lines
        private static void ReadPrintText(string path)
        {
            string[] readText = File.ReadAllLines(path);
            int i;
            for (i = 0; i < readText.Length; i++)
            {
                Console.WriteLine(readText[i]);
            }
        }

        // create a new file and write the input into it
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
