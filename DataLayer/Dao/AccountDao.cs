using LogicLayer;
using LogicLayer.interfaces;
using Minesweeper.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class AccountDao : IAccountDao
    {
        private readonly DatabaseConnection _databaseConnection;
        public AccountDao(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public bool CreateUser(string username, string password)
        {
            try
            {
                _databaseConnection.OpenConnection();
                string checkQuery = "SELECT COUNT(*) FROM user WHERE name = @userName";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, _databaseConnection.myConnection))
                {
                    checkCmd.Parameters.AddWithValue("@userName", username);
                    int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (userCount > 0)
                    {
                        Console.WriteLine("Gebruikersnaam bestaat al.");
                        return false;
                    }
                }
                string insertQuery = "INSERT INTO user (name, password) VALUES (@userName, @password)";

                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, _databaseConnection.myConnection))
                {
                    insertCmd.Parameters.AddWithValue("@userName", username);
                    insertCmd.Parameters.AddWithValue("@password", password);
                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine("Gebruiker succesvol toegevoegd.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                _databaseConnection.CloseConnection();
            }
            return false;
        }
        public bool SearchAccount(string userName, string passWord)
        {
            try
            {
                _databaseConnection.OpenConnection();
                string query = "SELECT * FROM user WHERE password = @passWord AND name = @userName";

                using (MySqlCommand cmd = new MySqlCommand(query, _databaseConnection.myConnection))
                {
                    cmd.Parameters.AddWithValue("@passWord", passWord);
                    cmd.Parameters.AddWithValue("@userName", userName);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"User ID: {reader["user_id"]}, User Name: {reader["name"]}");
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het uitvoeren van de query: {ex.Message}");
            }
            finally
            {
                _databaseConnection.CloseConnection();
            }
            return false;
        }
    }
}
