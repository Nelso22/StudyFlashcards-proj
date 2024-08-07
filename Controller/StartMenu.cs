using System;
using flashcards.UI;
using Spectre.Console;

namespace flashcards.Controller
{
    internal class StartMenu
    {
        // ManageStacks manageStacks = new ManageStacks();
        internal static void MainMenu()
        {
            bool closeApp = false;
            while (!closeApp)
            {

                Console.Clear();

                AnsiConsole.Write(
    new FigletText("Welcome!")
        .LeftJustified()
        .Color(Color.Cyan1));



                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nMAIN MENU\n\n[#00ffff]Please choose from the following options[/]")
                        .AddChoices("Select 0 to Close App", "Select 1 to Manage Stacks", "Select 2 to Manage Flashcards", "Select 3 to Study", "Select 4 to View Study Session Data"));

                switch (choice)
                {
                    case "Select 0 to Close App":
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    case "Select 1 to Manage Stacks":
                        //Method for Managing Stacks
                        ManageStacks.StackMenuOptions();
                        break;

                    case "Select 2 to Manage Flashcards":
                        // Add your method call here for managing flashcards
                        break;

                    case "Select 3 to Study":
                        // Add your method call here for studying
                        break;

                    case "Select 4 to View Study Session Data":
                        // Add your method call here for viewing study session data
                        break;

                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;

                }
            }
        }


    }
}
