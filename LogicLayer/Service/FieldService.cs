using LogicLayer.Dto;
using LogicLayer.interfaces;
using System.Collections.Generic;

namespace LogicLayer.Service
{
    public class FieldService: IFieldService
    {
        private readonly FieldDataAccess _fieldDataAccess;
        private readonly AddMines _addMines;
        private readonly FieldGenerator _fieldGenerator;
        private readonly MinesCounter _minesCounter;
        private readonly CellRevealer _cellRevealer;
        private readonly IFieldDao _fieldDao;
        public FieldService(FieldGenerator fieldGenerator,AddMines addMines, FieldDataAccess fieldDataAccess, MinesCounter minesCounter, CellRevealer cellRevealer, IFieldDao fieldDao)
        {
            _fieldGenerator = fieldGenerator;
            _minesCounter = minesCounter;
            _fieldDataAccess = fieldDataAccess;
            _addMines = addMines;
            _cellRevealer = cellRevealer;
            _fieldDao = fieldDao;
        }
        public Cell[,] GenerateField(int horizontal, int vertical, decimal minePercent)
        {
            var field = _fieldGenerator.GenerateField(horizontal, vertical);
            _addMines.MinePlacer(field, minePercent);
            _minesCounter.MinesAroundEachCell(field);
            _fieldDataAccess.StoreField(field);
            return field;
        }

        public List<CellDto> GetField()
        {
            return _fieldDataAccess.GetField();
        }
        public void RevealCell(int row, int col)
        {
            var fieldDto = _fieldDao.GetField();
            fieldDto = _cellRevealer.RevealCell(fieldDto, row, col);
            var field = ConvertToCellArray(fieldDto);
            _fieldDataAccess.StoreField(field);
        }

        private Cell[,] ConvertToCellArray(List<CellDto> fieldDto)
        {
            int horizontalSize = fieldDto.Max(c => c.Horizontal) + 1;
            int verticalSize = fieldDto.Max(c => c.Vertical) + 1;

            Cell[,] field = new Cell[horizontalSize, verticalSize];

            foreach (var cellDto in fieldDto)
            {
                field[cellDto.Horizontal, cellDto.Vertical] = new Cell(
                    cellDto.Horizontal,
                    cellDto.Vertical,
                    cellDto.IsMine,
                    cellDto.IsVisible,
                    cellDto.AmountOfMinesAroundCell
                );
            }

            return field;
        }

    }
}
