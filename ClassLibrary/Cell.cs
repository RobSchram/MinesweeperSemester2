using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    internal class Cell
    {
        public bool isVisible;
        public bool isMine;
        public int amountOfMinesAroundCell;

        public Cell(bool isVisible, bool isMine)
        {
            this.isVisible = isVisible;
            this.isMine = isMine;
        }

        public int MinesArounCell()
        {
            return amountOfMinesAroundCell;
        }
    }
}
