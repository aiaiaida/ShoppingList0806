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
                    Console.WriteLine("File Found!");
                    Console.WriteLine("The shopping list already has the following items");
                    ReadPrintText(path);
                    Console.WriteLine("Do you want to delete it? yes / no");
                    string deleteOrNot = Console.ReadLine();
                    if (deleteOrNot.ToLower() == "yes")
                    {
                        Console.WriteLine("Deleted successfully! Now you can create a new list by adding an item to it!");
                        string userInputAfterDel = AskingForInput(out addItems);
                        CreateAndWriteFile(path, userInputAfterDel);
                    }
                    else
                    {
                        string userInput = AskingForInput(out addItems);
                        File.AppendAllText(path, userInput + Environment.NewLine);
                    }

                }
                // if file doesn't exit, ask for user input
                // create a new file and write into it 
                else
                {
                    Console.WriteLine("File not found! Creating one...");
                    string userInput = AskingForInput(out addItems);
                    CreateAndWriteFile(path, userInput);
                }

                // return all the items to user after input
                Console.WriteLine("Your shopping list looks like this now!");
                ReadPrintText(path);
                Console.WriteLine("Press any key to continue.");
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
                //add creation date
                byte[] infoDate = new UTF8Encoding(true).GetBytes(Convert.ToString(DateTime.Now));
                fs.Write(infoDate, 0, infoDate.Length);
                //add a new line
                byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                fs.Write(newline, 0, newline.Length);
                //add input
                byte[] info = new UTF8Encoding(true).GetBytes(userInput);
                fs.Write(info, 0, info.Length);
                //add another new line
                fs.Write(newline, 0, newline.Length);
            }
        }
    }
}
