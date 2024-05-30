using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class MinesCounter
    {
        public void MinesAroundEachCell(Cell[,] field)
        {
            int horizontal = field.GetLength(0);
            int vertical = field.GetLength(1);
            for (int h = 0; h < horizontal; h++)
            {
                for (int v = 0; v < vertical; v++)
                {
                    int count = 0;
                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {
                            if (xOffset == 0 && yOffset == 0) continue;

                            int neighborX = h + xOffset;
                            int neighborY = v + yOffset;

                            if (neighborX >= 0 && neighborX < horizontal && neighborY >= 0 && neighborY < vertical)
                            {
                                Cell cellViewMine = field[neighborX, neighborY];
                                if (cellViewMine.isMine == 1) count++;
                            }
                        }
                    }
                    Cell cellView = field[h, v];
                    if (cellView.isMine != 1)
                    {
                        cellView.amountOfMinesAroundCell = count;
                    }
                }
            }
        }
    }
}
