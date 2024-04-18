using LogicLayer;
using LogicLayer.interfaces;
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

        public Field(int minePercent, int horizantal , int vertical)
        {
            this.minePercent = minePercent;
            this.horizontal = horizantal;
            this.vertical = vertical;
        }
        public void Fieldgenerator()
        {
            IStoreData storeData = null;
            for (int h = 0; h < horizontal; h++)
            {
                for (int v = 0; h < vertical; v++)
                {
                    storeData.StoreCell(h, v, 0, 0, 0);
                }
            }
            return;
        }
        public void MineGenerator()
        {
            IUpdateData updateData = null;
            amountOfMines = (horizontal * vertical) * minePercent;
            Random random = new Random();
            for (int i = 0; i < amountOfMines; i++)
            {
                int horizontalIndex = random.Next(0, horizontal);
                int verticalIndex = random.Next(0, vertical);
                updateData.UpdateCell(1, 0, 0);
            }
        }
        public void MinesArounEachCell()
        {
            IUpdateData updateData = null;
            ICellDao cell = null;
            for(int HorizontalIndex = 0;HorizontalIndex< horizontal; HorizontalIndex++)
            {
                for (int verticalIndex = 0; verticalIndex < vertical; verticalIndex++)
                {
                    int count = 0;
                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {
                            // Skip the current cell itself
                            if (xOffset == 0 && yOffset == 0) continue;

                            int neighborX = HorizontalIndex + xOffset;
                            int neighborY = verticalIndex + yOffset;

                            // Check if neighbor cell is within bounds
                            if (neighborX >= 0 && neighborX < horizontal && neighborY >= 0 && neighborY < vertical)
                            {
                                var cellVieuwMine = cell.GetCell(neighborX, neighborY);
                                if (cellVieuwMine.IsMine == 1)count++;
                            }
                        }
                    }
                    var cellVieuw = cell.GetCell(HorizontalIndex, verticalIndex);
                    if (cellVieuw.IsMine != 1)
                    {
                        cellVieuw.AmountOfMinesAroundCell = count;
                        int notMine = 0;
                        int notVisible = 0;
                        updateData.UpdateCell(notMine, notVisible,count);
                    }
                }
            }
        }
    }
}
