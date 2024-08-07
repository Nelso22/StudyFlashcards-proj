using System.Configuration;
using flashcards.Controller;
using flashcards.DatabaseConfig;
using Microsoft.VisualBasic;

namespace flashcards;

public class Program
{

  static string connectionString = ConfigurationManager.ConnectionStrings["Mykey"].ConnectionString;

  static string connectionString2 = ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;



  static void Main(string[] args)
  {
    ConfigureLaunch configureLaunch = new ConfigureLaunch();
    configureLaunch.CreateDb(connectionString);

    DatabaseManager databaseManager = new DatabaseManager();
    databaseManager.CreateTable(connectionString2);

    StartMenu startMenu = new StartMenu();

    StartMenu.MainMenu();

  }
}