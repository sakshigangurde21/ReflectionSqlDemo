using System;
using System.Reflection;
using ReflectionSqlLibrary;  // reference to your library
using Microsoft.Data.SqlClient;

namespace ReflectionSqlConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // SQL Server connection string
            string connectionString = @"Data Source=localhost;Initial Catalog=SubjectDB;User ID=sakshi21;Password=sak123;Trust Server Certificate=True";

            // Get type of Subject class
            Type type = typeof(Subject);
            string tableName = type.Name;

            // Start building CREATE TABLE query
            string createTableQuery = $"CREATE TABLE {tableName} (";

            // Read properties using Reflection
            PropertyInfo[] properties = type.GetProperties();
            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<ColumnAttribute>();
                if (attr != null)
                {
                    createTableQuery += $"{attr.Name} {attr.Type}, ";
                }
            }

            // Remove last comma and add closing bracket
            createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";

            // Print the query to console
            Console.WriteLine("Generated SQL Query:");
            Console.WriteLine(createTableQuery);

            // Execute the query in SQL Server
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(createTableQuery, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            Console.WriteLine("Table created successfully!");
        }
    }
}
