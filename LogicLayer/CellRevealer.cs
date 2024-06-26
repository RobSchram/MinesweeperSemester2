﻿using System.Collections.Generic;
using LogicLayer.Dto;
using LogicLayer.interfaces;

namespace LogicLayer.Service
{
    public class CellRevealer
    {
        readonly ICellDao _cellDao;
        public CellRevealer(ICellDao cellDao)
        {
            _cellDao = cellDao;
        }
        public void RevealCell(Cell cell , Cell[,] MineField)
        {
            if (cell != null)
            {
                cell.MakeCellVisible();
                _cellDao.UpdateCell(cell);

                if (cell.AmountOfMinesAroundCell == 0 && cell.IsMine != 1)
                {
                    RevealAdjacentCells(MineField, cell);
                }
            }
            return;
        }

        private void RevealAdjacentCells(Cell[,] field, Cell cell)
        {
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);

            for (int r = cell.Horizontal - 1; r <= cell.Horizontal + 1; r++)
            {
                for (int c = cell.Vertical - 1; c <= cell.Vertical + 1; c++)
                {
                    if (r >= 0 && r < rows && c >= 0 && c < cols && !(r == cell.Horizontal && c == cell.Vertical))
                    {
                        var newCell = field[r, c];
                        if (newCell.IsVisible == 0 && newCell.IsMine == 0)
                        {
                            newCell.MakeCellVisible();
                            _cellDao.UpdateCell(newCell);
                            if (newCell.AmountOfMinesAroundCell == 0)
                            {
                                RevealAdjacentCells(field, newCell);
                            }
                        }
                    }
                }
            }
        }
    }
}
