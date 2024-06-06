using LogicLayer.Dto;

namespace LogicLayer.interfaces
{
    public interface IFieldService
    {
        public Cell[,] GenerateField(int gameId, int horizontal, int vertical, decimal minePercent);
        public FieldDto GetField(int gameId);
        public string GetGameStatus(int gameId);
        public void RevealCell(int gameId, int horizontal, int vertical);
    }
}
