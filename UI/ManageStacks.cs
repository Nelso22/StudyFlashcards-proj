using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flashcards.Controller;
using flashcards.Models;
using Spectre.Console;

namespace flashcards.UI
{
    internal class ManageStacks
    {
        StackController stackController = new();

        internal static void StackMenuOptions()
        {
            ManageStacks manageStacks = new ManageStacks();

            bool goBack = false;
            while (goBack == false)
            {

                Console.Clear();


                var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[dodgerblue2]Please choose from the following options[/]")
                    .AddChoices(new[] {
                       "Select 0 to Return to Main Menu", "Select 1 Add Stack", "Select 2 Delete Stack", "Select 3 to Update name of a stack",
                    })
                );


                switch (userChoice)
                {
                    case "Select 0 to Return to Main Menu":
                        goBack = true;
                        StartMenu.MainMenu();
                        break;

                    case "Select 1 Add Stack":
                        manageStacks.AddStack();
                        break;

                    case "Select 2 Delete Stack":
                        manageStacks.Delete();
                        break;

                    case "Select 3 to Update name of a stack":
                        manageStacks.editStackName();
                        break;

                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                }

            }


        }

        private void AddStack()
        {

            var newStackName = AnsiConsole.Prompt(
        new TextPrompt<string>("[red][/]Please enter a [yellow]Stack name[/] or enter 0 to return to the Main Menu")
        .AllowEmpty().Validate(name => !doesStackExists(name.Trim()), "That stack already exists"));

            Console.WriteLine();


            if (newStackName.Trim() == "0")
            {
                StartMenu.MainMenu();
            }

            Stack stack = new Stack();

            stack.StackName = newStackName;

            if (AnsiConsole.Confirm($"Are you sure you want to add a stack with the name ${stack.StackName}"))
            {

                stackController.Post(stack);
                Console.WriteLine("Stack successfully added. Press any key to continue");
                Console.ReadLine();


            }

            ManageStacks.StackMenuOptions();



        }

        private void Delete()
        {
            var stacks = stackController.GetStacks();

            if (stacks.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No stacks available to delete.[/]");
                Console.WriteLine("Press any key to return to the menu");
                Console.ReadLine();
                ManageStacks.StackMenuOptions();
                return;
            }

            var selectedOption = new SelectionPrompt<Stack>();
            selectedOption.Title("Please choose from the following options");
            selectedOption.AddChoices(stacks);
            selectedOption.AddChoice(new Stack { StackId = 0, StackName = "Return to main menu" });
            selectedOption.UseConverter(stack => stack.StackName);

            var selectedStack = AnsiConsole.Prompt(selectedOption);

            if (selectedStack.StackId == 0) return;

            if (AnsiConsole.Confirm($"Are you sure you want to delete the stack ${selectedStack.StackName}?"))
            {
                stackController.Delete(selectedStack);
                Console.WriteLine("Press any key to return to the menu");
                Console.ReadLine();
                ManageStacks.StackMenuOptions();
            }

            else
            {
                ManageStacks.StackMenuOptions();
            }
        }

        private void editStackName()
        {
            var stacks = stackController.GetStacks();

            if (stacks.Count == 0)
            {
                Console.WriteLine("\n\nNo stacks available to edit.\n\n");
                Console.ReadLine();
                return;
            }

            var selectedStack = AnsiConsole.Prompt(
                new SelectionPrompt<Stack>()
                    .Title("Please select the stack you want to edit:")
                    .AddChoices(stacks)
                    .UseConverter(stack => stack.StackName)
            );

            if (selectedStack.StackId == 0) return;

            var newStackName = AnsiConsole.Prompt(
                new TextPrompt<string>("Please enter the new name for the selected stack:")
                    .Validate(name => !doesStackExists(name.Trim()), "That stack name already exists")
            );

            selectedStack.StackName = newStackName;

            stackController.Update(selectedStack);

            Console.WriteLine("Press any key to return to the menu");
            Console.ReadLine();

        }


        private bool doesStackExists(string newstackName)
        {
            var currentStacks = stackController.GetStacks();
            var stackExists = false;

            foreach (var stack in currentStacks)
            {
                if (newstackName.ToLower() == stack.StackName.ToLower())
                    stackExists = true;
            }

            return stackExists;

        }

    }
}

