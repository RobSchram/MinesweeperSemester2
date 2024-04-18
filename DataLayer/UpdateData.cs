using Minesweeper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UpdateData
    {
        public void UpdateCell(int isMine, int isVisible, int amountOfMinesAroundCell)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.OpenConnection();
            string query = $"UPDATE Cells SET IsMine = {isMine}, IsVisible = {isVisible}, AmountOfMinesAround = {amountOfMinesAroundCell}";
            databaseConnection.ExecuteQuery(query);
        }
    }
}
