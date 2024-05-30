using LogicLayer.Dto;
using LogicLayer.interfaces;
using System.Collections.Generic;

namespace LogicLayer.Service
{
    public class FieldService: IFieldService
    {
        private readonly FieldDataAccess _fieldDataAccess;
        private readonly CellRevealer _cellRevealer;
        private readonly IFieldDao _fieldDao;
        public FieldService(FieldDataAccess fieldDataAccess,CellRevealer cellRevealer, IFieldDao fieldDao)
        {
            _fieldDataAccess = fieldDataAccess;
            _cellRevealer = cellRevealer;
            _fieldDao = fieldDao;
        }
        public Cell[,] GenerateField(int horizontal, int vertical, decimal minePercent)
        {

            var generator = new FieldGenerator();
            var field = generator.GenerateField(horizontal, vertical);

            var placer = new AddMines();
            placer.MinePlacer(field, minePercent);

            var counter = new MinesCounter();
            counter.MinesAroundEachCell(field);

            _fieldDataAccess.StoreField(field);

            return field;
        }

        public FieldDto GetField()
        {
            return _fieldDataAccess.GetField();
        }
        public void RevealCell(int row, int col)
        {
            var fieldDto = _fieldDao.GetField();
            Field field = ConvertToField(fieldDto);
            _cellRevealer.RevealCell(field.MineField[row, col], field.MineField);
        }

        private Field ConvertToField(FieldDto fieldDto)
        {
            Field field = new Field(20, fieldDto.Horizontal, fieldDto.Vertical);

            foreach (CellDto cellDto in fieldDto.MineField)
            {
                field.MineField[cellDto.Horizontal, cellDto.Vertical] = new Cell(
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
