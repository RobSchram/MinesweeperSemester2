using Minesweeper.Data;
using LogicLayer.interfaces;
namespace DataLayer
{
    public class StoreData : IStoreData
    {
        public void StoreCell(int horizontal, int vertical,int id, int isMine, int isVisible, int amountOfMinesAroundCell)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.OpenConnection();
            string query = $"INSERT INTO Cells (horizontal, vertical, IsMine, IsVisible, AmountOfMinesAround) VALUES ({horizontal}, {vertical}, {isMine}, {isVisible}, {amountOfMinesAroundCell})";
            databaseConnection.ExecuteQuery(query);

        }
    }
}

