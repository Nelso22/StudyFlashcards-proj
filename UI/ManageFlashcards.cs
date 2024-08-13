using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flashcards.Controller;
using flashcards.Models;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;

namespace flashcards.UI
{
    internal class ManageFlashcards
    {
        StackController stackController = new();
        FlashCardController flashCardController = new();

        // ManageStacks manageStacks = new ManageStacks();


        Stack currentStack = new Stack();
        internal void FlashcardMenu(Stack stack)
        {
            currentStack = stack;
            var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[dodgerblue2]Please choose from the following options[/]")
                    .AddChoices(new[] {
                       "Main Menu,", "Add Flashcard", "Delete a Flashcard", "Edit flashcard", "Change Stack"
                    })
                );


            switch (userChoice)
            {
                case "Return to Main Menu":
                    StartMenu.MainMenu();
                    break;

                case "Add Flashcard":
                    AddCard();
                    break;

                case "Delete a Flashcard":
                    Delete();
                    break;

                case "Edit flashcard":
                    break;

                default:
                    Console.WriteLine("\nInvalid Command. Please select a option from 0 to 4.\n");
                    break;

            }

        }

        private void AddCard()
        {
            string question = AnsiConsole.Prompt(
                           new TextPrompt<string>("Create a Question you would like to make into a flashcard or press 0 to return to the Main Menu")
                               .Validate(input =>
                               {
                                   if (string.IsNullOrWhiteSpace(input))
                                   {
                                       return ValidationResult.Error("[red]Question cannot be empty or null.[/]");
                                   }
                                   else if (input.Trim() == "0")
                                   {
                                       StartMenu.MainMenu();

                                   }
                                   return ValidationResult.Success();
                               })

                       );

            string answer = AnsiConsole.Prompt(
                new TextPrompt<string>($"Please enter the answer for the following question: {question} - ")
                    .Validate(input =>
                    {
                        if (string.IsNullOrWhiteSpace(input))
                        {
                            return ValidationResult.Error("[red]Answer cannot be empty or null.[/]");
                        }
                        return ValidationResult.Success();
                    })
            );

            if (AnsiConsole.Confirm($"Are you sure you want to add this flashcard?"))
            {

                flashCardController.Post(new FlashcardDto
                {
                    StackId = currentStack.StackId,
                    Question = question,
                    Answer = answer
                });
                Console.WriteLine("Stack successfully added. Press any key to continue");
                Console.ReadLine();

            }

            else
            {
                StartMenu.MainMenu();
            }

        }

        private void Delete()
        {
            var allFlashcards = flashCardController.GetFlashCards(currentStack);
            var optionSelect = new SelectionPrompt<Flashcard>();
            optionSelect.Title($"Select one of the following flashcards to delete from the {currentStack.StackName} stack");
            optionSelect.AddChoices(allFlashcards);
            optionSelect.AddChoice(new Flashcard { FlashcardId = 0, Question = "Return to Main Menu" });
            optionSelect.UseConverter(flashcard => flashcard.Question);

            var cardToDelete = AnsiConsole.Prompt(optionSelect);

            if (cardToDelete.FlashcardId == 0)
            {
                StartMenu.MainMenu();
                return;
            }

            if (AnsiConsole.Confirm($"Are you sure you want to delete the Flashcard ${cardToDelete.Question} from {currentStack.StackName}?"))
            {
                flashCardController.Delete(cardToDelete);
                Console.WriteLine("Press any key to return to the menu");
                Console.ReadLine();
                StartMenu.MainMenu();
                Console.Clear();
            }
            Console.Clear();
            StartMenu.MainMenu();
        }

        private void EditCard()
        {

            var allCards = flashCardController.GetFlashCards(currentStack);

            if (allCards.Count == 0)
            {
                Console.WriteLine("\n\nNo cards available to edit.\n\n");
                Console.ReadLine();
                return;
            }

            var allFlashcards = flashCardController.GetFlashCards(currentStack);

            var optionSelect = new SelectionPrompt<Flashcard>();

            optionSelect.Title($"Select one of the following flashcards to edit from the {currentStack.StackName} stack");
            optionSelect.AddChoices(allFlashcards);
            optionSelect.AddChoice(new Flashcard { FlashcardId = 0, Question = "Return to Main Menu" });
            optionSelect.UseConverter(flashcard => $"{flashcard.Question} / {flashcard.Answer}");

            var selectedCard = AnsiConsole.Prompt(optionSelect);

            if (selectedCard.FlashcardId == 0) return;

            var newFlashcardQuestion = AnsiConsole.Prompt(
                new TextPrompt<string>("Please enter the new question for the selected flashcard:")
                    .Validate(name => !CheckFlashCardExists(name.Trim()), "That flashcard name already exists")
            );

            var newFlashcardAnswer = AnsiConsole.Prompt(
            new TextPrompt<string>("Please enter the new answer for the selected flashcard:")
                .Validate(answer => !CheckFlashCardExists(answer.Trim()), "That flashcard answer already exists")
        );

            newFlashcardQuestion = selectedCard.Question;
            newFlashcardQuestion = selectedCard.Answer;

            Console.WriteLine("Press any key to return to the menu");
            Console.ReadLine();

        }

        private bool CheckFlashCardExists(string front)
        {
            var flashCards = flashCardController.GetFlashCards(currentStack);
            var newFlashCardFound = false;

            foreach (var flashCard in flashCards)
            {
                if (front.ToLower() == flashCard.Question.ToLower())
                    newFlashCardFound = true;
            }

            return newFlashCardFound;
        }

    }
}



