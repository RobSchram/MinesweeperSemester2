using LogicLayer.Dto;
using LogicLayer.interfaces;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public class Field 
    {
        public int Horizontal {  get; }
        public int Vertical { get;  }
        public Cell[,] MineField { get;}

        public Field(int horizontal, int vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
            MineField = new Cell[horizontal, vertical];
        }
    }
}
