using LogicLayer.Dto;
using LogicLayer.interfaces;
using System.Collections.Generic;

namespace LogicLayer.Service
{
    public class FieldService: IFieldService
    {
        private readonly CellRevealer _cellRevealer;
        private readonly IFieldDao _fieldDao;
        public FieldService(CellRevealer cellRevealer, IFieldDao fieldDao)
        {
            _cellRevealer = cellRevealer;
            _fieldDao = fieldDao;
        }
        public Cell[,] GenerateField(int horizontal, int vertical, decimal minePercent)
        {

            var generator = new FieldGenerator();
            var field = generator.GenerateField(horizontal, vertical);

            Mines mines = new Mines();
            mines.Placer(field);

            mines.AroundEachCell(field);
            _fieldDao.ClearField();
            _fieldDao.StoreField(field);

            return field;
        }

        public FieldDto GetField()
        {
            return _fieldDao.GetField();
        }
        public string RevealCell(int row, int col)
        {
            GameProgress gameProgress = new GameProgress();
            var fieldDto = _fieldDao.GetField();
            Field field = ConvertToField(fieldDto);
            _cellRevealer.RevealCell(field.MineField[row, col], field.MineField);
            return gameProgress.GameStatus(field.MineField);
        }

        private Field ConvertToField(FieldDto fieldDto)
        {
            Field field = new Field(fieldDto.Horizontal, fieldDto.Vertical);

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
