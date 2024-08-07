using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using flashcards.Models;
using Microsoft.Data.SqlClient;

namespace flashcards.Controller
{
    internal class FlashCardController
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;

        internal void Post(FlashcardDto flashcard)
        {

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"INSERT INTO Flashcards (Question, Answer) VALUES ('{flashcard.Question}', '{flashcard.Answer}')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                    }

                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        internal void Delete(Flashcard flashcard)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"DELETE FROM Flashcards WHERE FlashcardId = '{flashcard.FlashcardId}'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine($"\n\nRecord with Id {flashcard.FlashcardId} was deleted. \n\n");

                    }
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        internal List<Flashcard> GetFlashCards(Stack stack)
        {
            List<Flashcard> tableData = new List<Flashcard>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $@"SELECT * FROM Flashcards WHERE StackId = '{stack.StackId}'  ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    tableData.Add(
                                        new Flashcard
                                        {
                                            FlashcardId = reader.GetInt32(0),
                                            StackId = reader.GetInt32(1),
                                            Question = reader.GetString(2),
                                            Answer = reader.GetString(3)
                                        }
                                    );
                                }
                            }
                            else
                            {
                                Console.WriteLine("No rows found.");
                            }
                            reader.Close();
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return tableData;
        }

    }
}