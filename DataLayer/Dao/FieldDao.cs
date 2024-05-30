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

        public FieldDto GetField()
        {
            var (horizontal, vertical)=GetFieldDimensions();
            FieldDto fieldDto = new FieldDto(horizontal , vertical);
            var query = "SELECT * FROM cell";
            try
            {
                _databaseConnection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, _databaseConnection.myConnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CellDto cell = new CellDto
                    {
                        Horizontal = (int)reader["horizontal"],
                        Vertical = (int)reader["vertical"],
                        IsMine = (int)reader["is_mine"],
                        IsVisible = (int)reader["is_visible"],
                        AmountOfMinesAroundCell = (int)reader["amount_of_mines_around_cell"]
                    };
                        fieldDto.MineField[cell.Horizontal, cell.Vertical] = cell;
                    
                }

                reader.Close();
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
        public void StoreField(Cell[,] mineField)
        {
            try
            {
                _databaseConnection.OpenConnection();

                foreach (Cell cell in mineField)
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
        public (int horizontal, int vertical) GetFieldDimensions()
        {
            int horizontal = 0;
            int vertical = 0;
            var queryHorizontal = "SELECT COUNT(DISTINCT horizontal) FROM cell";
            var queryVertical = "SELECT COUNT(DISTINCT vertical) FROM cell";

            try
            {
                _databaseConnection.OpenConnection();
                MySqlCommand cmdHorizontal = new MySqlCommand(queryHorizontal, _databaseConnection.myConnection);
                horizontal = Convert.ToInt32(cmdHorizontal.ExecuteScalar());
                MySqlCommand cmdVertical = new MySqlCommand(queryVertical, _databaseConnection.myConnection);
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
