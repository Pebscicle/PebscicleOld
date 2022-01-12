using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PebMaps.Services
{
    public class StateService
    {
        protected static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=USA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        public async Task<List<State>> GetUSStates()
        {
            string query = $"SELECT dbo.usStates.Abbreviation, dbo.usStates.Name, dbo.CountyPopulation.POPESTIMATE2020 FROM dbo.usStates INNER JOIN dbo.CountyPopulation ON dbo.usStates.Name = dbo.CountyPopulation.CTYNAME AND dbo.usStates.Name = dbo.CountyPopulation.STNAME;";
            var result = RunCommandAsynchronously(query, connectionString);
            return result;
        }

        private static List<State> RunCommandAsynchronously(
        string commandText, string connectionString)
        {
            var results = new List<State>();
            // Given command text and connection string, asynchronously execute
            // the specified command against the connection. For this example,
            // the code displays an indicator as it is working, verifying the
            // asynchronous behavior.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(commandText, connection);

                    connection.Open();
                    IAsyncResult result = command.BeginExecuteReader();

                    using (SqlDataReader reader = command.EndExecuteReader(result))
                    {
                        results = getResults(reader);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error ({0}): {1}", ex.Number, ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                catch (Exception ex)
                {
                    // You might want to pass these errors
                    // back out to the caller.
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                return results;
            }
        }

        private static List<State> getResults(SqlDataReader reader)
        {
            List<State> results = new List<State>();
            // Display the data within the reader.
            while (reader.Read())
            {
                var state = new State();
                state.Abbreviation = (string)reader.GetValue(0);
                state.Name = (string)reader.GetValue(1);
                state.Population = (int)reader.GetValue(2);
                results.Add(state);
            }
            return results;
        }

        public static string getPathFromAbbreviation(string abbr)
        {
            string query = $"SELECT dbo.StatePaths.Path FROM dbo.StatePaths WHERE dbo.StatePaths.Abbreviation = '{abbr}';";
            var result = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    IAsyncResult SQLresult = command.BeginExecuteReader();

                    using (SqlDataReader reader = command.EndExecuteReader(SQLresult))
                    {
                        while (reader.Read())
                        {
                            result = (string)reader.GetValue(0);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error ({0}): {1}", ex.Number, ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                catch (Exception ex)
                {
                    // You might want to pass these errors
                    // back out to the caller.
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
            return result;
        }
    }
}
