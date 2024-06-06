using LogicLayer.Dto;

namespace LogicLayer.interfaces
{
    public interface IFieldDao
    {
        public FieldDto GetField(int gameId);
        public void StoreField(Cell[,] mineField);
    }
}
