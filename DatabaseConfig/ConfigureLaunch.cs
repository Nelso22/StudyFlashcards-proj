using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace flashcards.DatabaseConfig
{
  internal class ConfigureLaunch
  {

    internal void CreateDb(string connectionString)
    {
      //Create sql query and declare as string

      string queryString = @"
                IF NOT EXISTS (
                    SELECT name 
                    FROM sys.databases 
                    WHERE name = 'FlashcardsDb'
                )
                BEGIN
                    CREATE DATABASE FlashcardsDb;
                    PRINT 'Database FlashcardsDb created successfully.';
                END
                ELSE
                BEGIN
                    PRINT 'Database FlashcardsDb already exists.';
                END";
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        SqlCommand command = new SqlCommand(
            queryString, connection);
        connection.Open();
        command.ExecuteNonQuery();
      };

    }

  }
}