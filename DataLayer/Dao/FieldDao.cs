using LogicLayer;
using LogicLayer.Dto;
using LogicLayer.interfaces;
using Minesweeper.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataLayer.Dao
{
    public class FieldDao : IFieldDao
    {
        private readonly DatabaseConnection _databaseConnection;
        public FieldDao(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public FieldDto GetField(int gameId)
        {
            var (horizontal, vertical) = GetFieldDimensions(gameId);
            FieldDto fieldDto = new FieldDto(horizontal, vertical);
            var query = "SELECT * FROM cell WHERE game_id = @gameId";
            try
            {
                _databaseConnection.OpenConnection();
                using (MySqlCommand cmd = new MySqlCommand(query, _databaseConnection.myConnection))
                {
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CellDto cell = new CellDto
                                {
                                    GameId = (int)reader["game_id"],
                                    Horizontal = (int)reader["horizontal"],
                                    Vertical = (int)reader["vertical"],
                                    IsMine = (int)reader["is_mine"],
                                    IsVisible = (int)reader["is_visible"],
                                    AmountOfMinesAroundCell = (int)reader["amount_of_mines_around_cell"]
                                };
                                fieldDto.MineField[cell.Horizontal, cell.Vertical] = cell;
                            }
                        }
                        else { Console.WriteLine("no field found"); }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _databaseConnection.CloseConnection();
            }

            return fieldDto;
        }

        public void StoreField(Cell[,] mineField)
        {
            try
            {
                _databaseConnection.OpenConnection();

                foreach (Cell cell in mineField)
                {
                    string query = "INSERT INTO Cell (game_id, horizontal, vertical, is_mine, is_visible, amount_of_mines_around_cell) " +
                                   "VALUES (@gameId, @horizontal, @vertical, @is_mine, @is_visible, @amount_of_mines_around_cell)";

                    using (MySqlCommand cmd = new MySqlCommand(query, _databaseConnection.myConnection))
                    {
                        cmd.Parameters.AddWithValue("@gameId", cell.GameId);
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
        public (int horizontal, int vertical) GetFieldDimensions(int gameId)
        {
            int horizontal = 0;
            int vertical = 0;
            var queryHorizontal = "SELECT COUNT(DISTINCT horizontal) FROM cell WHERE game_id = @gameId";
            var queryVertical = "SELECT COUNT(DISTINCT vertical) FROM cell WHERE game_id = @gameId";

            try
            {
                _databaseConnection.OpenConnection();
                
                MySqlCommand cmdHorizontal = new MySqlCommand(queryHorizontal, _databaseConnection.myConnection);
                cmdHorizontal.Parameters.AddWithValue("@gameId", gameId);
                horizontal = Convert.ToInt32(cmdHorizontal.ExecuteScalar());
                MySqlCommand cmdVertical = new MySqlCommand(queryVertical, _databaseConnection.myConnection);
                cmdVertical.Parameters.AddWithValue("@gameId", gameId);
                vertical = Convert.ToInt32(cmdVertical.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _databaseConnection.CloseConnection();
            }

            return (horizontal, vertical);
        }
    }
}
