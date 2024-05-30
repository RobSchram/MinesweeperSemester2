using Minesweeper.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.interfaces;

namespace DataLayer
{
    public class ClearData : IClearData
    {
        private readonly DatabaseConnection _databaseConnection;

        public ClearData(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }
        public void ClearField()
        {
            try
            {
                _databaseConnection.OpenConnection();
                string query = "DELETE FROM Cell";

                using (MySqlCommand cmd = new MySqlCommand(query, _databaseConnection.myConnection))
                {
                    cmd.ExecuteNonQuery();
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
        }
    }
}
