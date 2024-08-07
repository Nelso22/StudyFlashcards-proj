using System;
using System.Collections.Generic;
using System.Configuration;
using flashcards.Models;
using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace flashcards.Controller
{
    internal class StackController
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;

        internal void Post(Stack stack)
        {

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"INSERT INTO Stacks (StackName) VALUES ('{stack.StackName}')";

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

        internal void Delete(Stack stack)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"DELETE FROM Stacks WHERE StackId = '{stack.StackId}'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine($"\n\nRecord with Id {stack.StackId} was deleted. \n\n");

                    }
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        internal List<Stack> GetStacks()
        {
            List<Stack> tableData = new List<Stack>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = @"SELECT * FROM Stacks";

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
                                        new Stack
                                        {
                                            StackId = reader.GetInt32(0),
                                            StackName = reader.GetString(1),
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

        internal Stack GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sql = $"SELECT * FROM Stacks WHERE StackId = '{id}'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        Stack stack = new();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            stack.StackId = reader.GetInt32(0);
                            stack.StackName = reader.GetString(1);

                        }
                        Console.WriteLine("\n\n");

                        return stack;
                    }
                }

            }
        }

        internal void Update(Stack stack)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = @$"UPDATE Stacks SET 
                        StackName = '{stack.StackName}' 
                        WHERE StackId = {stack.StackId}";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"\n\nStack name with Id {stack.StackId} was updated.\n\n");
        }



    }

}