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
        public Cell[,] GenerateField(int horizontal, int vertical, decimal minePercent);
        public List<CellDto> GetField();
        public void RevealCell(int row, int col);
    }
}
