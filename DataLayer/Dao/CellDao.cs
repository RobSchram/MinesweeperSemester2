using Minesweeper.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Dto;
using LogicLayer.interfaces;

namespace DataLayer.Dao
{
    internal class CellDao : ICellDao
    {
        public CellDto GetCell(int horizontal, int vertical)
        {
            CellDto cell = null;
            var query = "Select* FROM cell WHERE x =@horizontal AND y = @vertical";
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(query);
            try
            {
                cmd.Parameters.AddWithValue("@horizontal", horizontal);
                cmd.Parameters.AddWithValue("@vertical", vertical);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    cell = new CellDto()
                    {
                        Horizontal = (int)reader["x"],
                        Vertical = (int)reader["y"],
                        IsMine = (int)reader["isMine"],
                        IsVisible = (int)reader["isvisible"],
                        AmountOfMinesAroundCell = (int)reader["amountOfMinesAroundCell"]
                    };

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cell;
        }
    }
}
