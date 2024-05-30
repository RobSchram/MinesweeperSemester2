using LogicLayer;
using LogicLayer.interfaces;
using Minesweeper.Data;
using MySql.Data.MySqlClient;
using System;

namespace DataLayer
{
    public class StoreData : IStoreData
    {
        private readonly DatabaseConnection _databaseConnection;

        public StoreData(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }

        public void StoreField(Cell[,] cells)
        {
            try
            {
                _databaseConnection.OpenConnection();

                foreach (Cell cell in cells)
                {
                    string query = "INSERT INTO Cell (horizontal, vertical, is_mine, is_visible, amount_of_mines_around_cell) " +
                                   "VALUES (@horizontal, @vertical, @is_mine, @is_visible, @amount_of_mines_around_cell)";

                    using (MySqlCommand cmd = new MySqlCommand(query, _databaseConnection.myConnection))
                    {
                        cmd.Parameters.AddWithValue("@horizontal", cell.Horizontal);
                        cmd.Parameters.AddWithValue("@vertical", cell.Vertical);
                        cmd.Parameters.AddWithValue("@is_mine", cell.IsMine);
                        cmd.Parameters.AddWithValue("@is_visible", cell.IsVisible);
                        cmd.Parameters.AddWithValue("@amount_of_mines_around_cell", cell.AmountOfMinesAroundCell);

                        cmd.ExecuteNonQuery();
                    }

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
