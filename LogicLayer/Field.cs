using LogicLayer.Dto;
using LogicLayer.interfaces;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public class Field 
    {
        public int horizontal;
        public int vertical;
        public decimal minePercent;
        public decimal amountOfMines;
        public Cell[,] field;

        public Field(int minePercent, int horizontal, int vertical)
        {
            this.minePercent = minePercent;
            this.horizontal = horizontal;
            this.vertical = vertical;
            field = new Cell[horizontal, vertical];
        }
    }
}
