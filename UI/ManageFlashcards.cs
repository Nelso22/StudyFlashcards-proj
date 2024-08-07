using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flashcards.Controller;
using Spectre.Console;

namespace flashcards.UI
{
    internal class ManageFlashcards
    {

        internal void FlashcardMenu()
        {

            var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[dodgerblue2]Please choose from the following options[/]")
                    .AddChoices(new[] {
                       "Select 0 to Return to Main Menu", "Select 1 Add Flashcard", "Select 2 Delete Flashcard", "Select 3 to edit flashcard",
                    })
                );


            switch (userChoice)
            {
                case "0":
                    StartMenu.MainMenu();
                    break;

                case "1":
                    //Method for Adding card
                    AddCard();
                    break;

                case "2":
                    // Add your method call here for managing flashcards
                    break;

                case "3":
                    // Add your method call here for studying
                    break;

                case "4":
                    // Add your method call here for viewing study session data
                    break;

                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                    break;

            }

        }

        internal void AddCard()
        {

        }




    }
}



