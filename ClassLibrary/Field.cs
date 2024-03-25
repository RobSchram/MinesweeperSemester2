using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    internal class Field
    {
        public int horizantal;
        public int vertical;
        public int minePercent;
        public int amountOfMines;
        Cell[,]field;

        public Field(int minePercent)
        {
            this.minePercent = minePercent;
        }
        public Cell[,] Fieldgenerator() 
        {
            for(int h = 0; h < horizantal; h++)
            {
                for (int v = 0; h < vertical; v++)
                {
                    field[horizantal, vertical] = new Cell(false, false);
                }
            }
            return field;
        }
        public void MineGenerator()
        {
            amountOfMines = (horizantal * vertical) * minePercent;
            for(int i = 0; i < amountOfMines;  i++)
            {
                Random random = new Random();
                int horizontalIndex=random.Next(0,horizantal);
                int verticalIndex =random.Next(0,vertical);
                field[horizontalIndex, verticalIndex] = new Cell(false, true);
            }
        }
    }
}
