using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flashcards.Controller
{
    internal class GetUserInput
    {

        StackController stackController = new StackController();

        internal void MainMenu()
        {

            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("MAIN MENU\n");
                Console.WriteLine("Please choose from the following options\n\n");
                Console.WriteLine("Type 0 to close App\n");
                Console.WriteLine("Type 1 to Manage Stacks\n");
                Console.WriteLine("Type 2 to Manage Flashcards\n");
                Console.WriteLine("Type 3 to Study\n");
                Console.WriteLine("Type 4 to View Study Session Data\n");

                string userInput = Console.ReadLine();

                while (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.");
                    userInput = Console.ReadLine();

                }

                switch (userInput)
                {
                    case "0":
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    case "1":
                        stackController.GetStackNames();
                        break;


                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to X.\n");
                        break;
                }
            }
        }
    }
}