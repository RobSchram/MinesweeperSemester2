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
        public int Horizontal { get; }
        public int Vertical { get; }
        public int IsMine { get; private set; }
        public int IsVisible {  get; private set; }
        public int AmountOfMinesAroundCell { get; private set; }
        public Cell(int horizontal, int vertical, int isMine, int isVisible, int amountOfMinesAroundCell) 
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
            this.IsMine = isMine;
            this.IsVisible = isVisible;
            this.AmountOfMinesAroundCell = amountOfMinesAroundCell;

        }
        public void MakeCellVisible()
        {
            IsVisible = 1;
            return;
        }
        public void MakeCellMine()
        {
            IsMine = 1;
            return;
        }
        public void SetMinesAroundCell(int amount)
        {
            if(amount<9 && amount>= 0)
            AmountOfMinesAroundCell = amount;
            return;
        }
    }
}
