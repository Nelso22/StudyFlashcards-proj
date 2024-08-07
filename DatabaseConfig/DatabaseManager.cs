using System;
using Microsoft.Data.SqlClient;

namespace flashcards.DatabaseConfig
{
    internal class DatabaseManager
    {
        internal void CreateTable(string connectionString)
        {
            // SQL queries to create tables in the FlashcardsDb database
            string[] queries = new string[] {
                @"
                USE FlashcardsDb;

                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Stacks')
                BEGIN
                    CREATE TABLE Stacks (
                        StackId INT IDENTITY(1,1) PRIMARY KEY,
                        StackName NVARCHAR(255) UNIQUE NOT NULL
                    );
                END;",

                @"
                USE FlashcardsDb;

                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Flashcards')
                BEGIN
                    CREATE TABLE Flashcards (
                        FlashcardId INT IDENTITY(1,1) PRIMARY KEY,
                        StackId INT NOT NULL,
                        Question NVARCHAR(255) NOT NULL,
                        Answer NVARCHAR(255) NOT NULL,
                        FOREIGN KEY (StackId) REFERENCES Stacks(StackId) ON DELETE CASCADE
                    );
                END;"
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    foreach (string query in queries)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
