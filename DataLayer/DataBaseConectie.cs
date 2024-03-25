using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace TALPA.Data;


public class DatabaseConnection
{
    string myConnectionString { get; set; }
    MySql.Data.MySqlClient.MySqlConnection myConnection { get; set; }

    public DatabaseConnection()
    {
        myConnectionString = "";
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

    public void QeuryDatabase(string Qeury)
    {
        try
        {
            myConnection = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
            //open a connection
            myConnection.Open();

            // create a MySQL command and set the SQL statement with parameters
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandText = Qeury;
            MySqlDataReader rdr = myCommand.ExecuteReader();


            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            }
            myConnection.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }
}