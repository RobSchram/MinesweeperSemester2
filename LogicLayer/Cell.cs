using LogicLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{

    public class Cell
    {
        public int horizontal;
        public int vertical;
        public int isMine;
        public int isVisible;
        public int amountOfMinesAroundCell;
        public Cell(int horizontal, int vertical, int isMine, int isVisible, int amountOfMinesAroundCell) 
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
            this.isMine = isMine;
            this.isVisible = isVisible;
            this.amountOfMinesAroundCell = amountOfMinesAroundCell;

        }
        public void MakeCellVisible()
        {
            isVisible = 1;
            return;
        }
        public void MakeCellMine()
        {
            isMine = 1;
            return;
        }
    }
}
