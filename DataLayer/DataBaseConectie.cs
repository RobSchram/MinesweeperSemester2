using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace Minesweeper.Data;


public class DatabaseConnection
{
    public string myConnectionString { get; set; }
    public MySql.Data.MySqlClient.MySqlConnection myConnection { get; set; }

    public DatabaseConnection()
    {
        myConnectionString = "Server=studmysql01.fhict.local;Uid=dbi531101;Database=dbi531101;Pwd=123hello;";
        myConnection = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
    }

    public void OpenConnection()
    {
        myConnection.Open();
    }

    public void CloseConnection()
    {
        myConnection.Close();
    }
    public void ExecuteQuery(string query)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand(query, myConnection);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Query uitgevoerd: " + query);
        }
        catch (MySqlException ex)
        {
            // Toon een foutmelding als er een probleem optreedt bij het uitvoeren van de query
            Console.WriteLine($"Fout bij het uitvoeren van de query: {ex.Message}");
        }
    }
}