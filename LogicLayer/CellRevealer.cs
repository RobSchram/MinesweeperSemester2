using System.Collections.Generic;
using System.Linq;
using LogicLayer.Dto;

namespace LogicLayer.Service
{
    public class CellRevealer
    {
        public List<CellDto> RevealCell(List<CellDto> field, int row, int col)
        {
            var cell = field.FirstOrDefault(c => c.Vertical == row && c.Horizontal == col);
            if (cell != null)
            {
                cell.IsVisible = 1;
                if (cell.AmountOfMinesAroundCell == 0)
                {
                    RevealAdjacentCells(field, row, col);
                }
            }
            return field;
        }

        private void RevealAdjacentCells(List<CellDto> field, int row, int col)
        {
            var adjacentCells = field.Where(c =>
                c.Vertical >= row - 1 && c.Vertical <= row + 1 &&
                c.Horizontal >= col - 1 && c.Horizontal <= col + 1 &&
                !(c.Vertical == row && c.Horizontal == col)).ToList();

            foreach (var cell in adjacentCells)
            {
                if (cell.IsVisible == 0 && cell.IsMine == 0)
                {
                    cell.IsVisible = 1;

                    if (cell.AmountOfMinesAroundCell == 0)
                    {
                        RevealAdjacentCells(field, cell.Vertical, cell.Horizontal);
                    }
                }
            }
        }
    }
}
