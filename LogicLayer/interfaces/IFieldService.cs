using LogicLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.interfaces
{
    public interface IFieldService
    {
        public Cell[,] GenerateField(int gameId, int horizontal, int vertical, decimal minePercent);
        public FieldDto GetField(int gameId);
        public string GetGameStatus(int gameId);
        public void RevealCell(int gameId, int row, int col);
    }
}
