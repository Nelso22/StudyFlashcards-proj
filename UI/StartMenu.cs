using System;
using flashcards.UI;
using Spectre.Console;

namespace flashcards.Controller
{
    internal class StartMenu
    {

        internal static void MainMenu()
        {
            ManageFlashcards manageFlashcards = new ManageFlashcards();
            ManageStacks manageStacks = new ManageStacks();

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
                        .AddChoices("Close App", "Manage Stacks", "Manage Flashcards", "Study", "View Study Session Data"));

                switch (choice)
                {
                    case "Close App":
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    case "Manage Stacks":
                        //Method for Managing Stacks
                        ManageStacks.StackMenuOptions();
                        break;

                    case "Manage Flashcards":

                        manageStacks.EditStack();
                        break;

                    case "Study":
                        // Add your method call here for studying
                        break;

                    case "To View Study Session Data":
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
