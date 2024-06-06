using LogicLayer;
using LogicLayer.interfaces;
using Minesweeper.Data;
using MySql.Data.MySqlClient;

namespace DataLayer.Dao
{
    public class CellDao : ICellDao
    {
        public void UpdateCell(Cell cell)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            try
            {
                databaseConnection.OpenConnection();
                string query = "UPDATE Cell SET is_visible = @IsVisible WHERE game_id = @gameId AND horizontal = @Horizontal AND vertical = @Vertical";

                using (MySqlCommand cmd = new MySqlCommand(query, databaseConnection.myConnection))
                {
                    cmd.Parameters.AddWithValue("@IsVisible", cell.IsVisible);
                    cmd.Parameters.AddWithValue("@Horizontal", cell.Horizontal);
                    cmd.Parameters.AddWithValue("@Vertical", cell.Vertical);
                    cmd.Parameters.AddWithValue("@gameId", cell.GameId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het uitvoeren van de query: {ex.Message}");
            }
            finally
            {
                databaseConnection.CloseConnection();
            }
        }
    }
}
