using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class FieldGenerator
    {
        public Cell[,] GenerateField(int horizontal , int vertical)
        {
            var field = new Cell[horizontal, vertical];
            for (int h = 0; h < horizontal; h++)
            {
                for (int v = 0; v < vertical; v++)
                {
                    field[h, v] = new Cell(h, v, 0, 0, 0);
                }
            }
            return field;
        }

    }
}
