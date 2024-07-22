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
        static string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;

        internal void GetStackNames()
        {
            List<StackDto> tableData = new List<StackDto>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = @"SELECT StackName FROM Stacks";

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
                                        new StackDto
                                        {
                                            StackName = reader.GetString(0),
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

                // Create a Spectre.Console table
                var table = new Table();
                table.Border = TableBorder.Ascii;
                table.AddColumn("Names");

                // Add rows to the table
                foreach (var stack in tableData)
                {
                    table.AddRow(stack.StackName);
                }

                // Render the table to the console
                AnsiConsole.Write(table);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
