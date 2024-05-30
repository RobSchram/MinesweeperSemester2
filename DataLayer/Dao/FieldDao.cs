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

        public List<CellDto> GetField()
        {
            List<CellDto> cellDtos = new List<CellDto>();
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
                    cellDtos.Add(cell);
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
            return cellDtos;
        }
    }
}
