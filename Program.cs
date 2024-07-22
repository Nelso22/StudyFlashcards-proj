using System.Configuration;
using flashcards.Controller;
using flashcards.DatabaseConfig;
using Microsoft.VisualBasic;

namespace flashcards;

public class Program
{

  static string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;


  static void Main(string[] args)
  {
    ConfigureLaunch configureLaunch = new ConfigureLaunch();
    configureLaunch.CreateDb(connectionString);

    DatabaseManager databaseManager = new DatabaseManager();
    databaseManager.CreateTable(connectionString);

    GetUserInput getUserInput = new GetUserInput();
    getUserInput.MainMenu();


  }
}