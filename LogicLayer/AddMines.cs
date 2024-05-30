using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class AddMines
    {
        public void MinePlacer(Cell[,] field, decimal minePercent)
        {
            int horizontal = field.GetLength(0);
            int vertical = field.GetLength(1);
            minePercent = 0.2m;
            decimal amountOfMines = Convert.ToDecimal( horizontal * vertical)*minePercent;

            Random random = new Random();
            for (int i = 0; i < amountOfMines; i++)
            {
                int horizontalIndex = random.Next(0, horizontal);
                int verticalIndex = random.Next(0, vertical);
                if (field[horizontalIndex, verticalIndex].IsMine == 1)
                {
                    i--;
                }
                else
                {
                    field[horizontalIndex, verticalIndex].MakeCellMine();
                }
            }
        }
    }
}
