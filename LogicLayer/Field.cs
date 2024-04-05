using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Field
    {
        public int horizontal;
        public int vertical;
        public int minePercent;
        public int amountOfMines;
        Cell[,] field;

        public Field(int minePercent, int horizantal , int vertical)
        {
            this.minePercent = minePercent;
            this.horizontal = horizantal;
            this.vertical = vertical;
        }
        public Cell[,] Fieldgenerator()
        {
            for (int h = 0; h < horizontal; h++)
            {
                for (int v = 0; h < vertical; v++)
                {
                    field[horizontal, vertical] = new Cell(false, false);
                }
            }
            return field;
        }
        public void MineGenerator()
        {
            amountOfMines = (horizontal * vertical) * minePercent;
            for (int i = 0; i < amountOfMines; i++)
            {
                Random random = new Random();
                int horizontalIndex = random.Next(0, horizontal);
                int verticalIndex = random.Next(0, vertical);
                field[horizontalIndex, verticalIndex] = new Cell(false, true);
            }
        }
        public void MinesArounEachCell()
        {
            for(int i = 0;i< horizontal; i++)
            {
                for (int j = 0; j < vertical; j++)
                {
                    int count = 0;
                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {
                            // Skip the current cell itself
                            if (xOffset == 0 && yOffset == 0) continue;

                            int neighborX = i + xOffset;
                            int neighborY = j + yOffset;

                            // Check if neighbor cell is within bounds
                            if (neighborX >= 0 && neighborX < horizontal && neighborY >= 0 && neighborY < vertical)
                            {
                                if (field[neighborX, neighborY].isMine) count++;
                            }
                        }
                    }
                    field[i, j].amountOfMinesAroundCell = count;
                }
            }
        }
    }
}
