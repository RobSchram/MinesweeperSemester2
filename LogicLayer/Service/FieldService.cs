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
            _fieldDao = fieldDao;
            _cellRevealer = cellRevealer;
        }

        public Cell[,] GenerateField(int gameId,int horizontal, int vertical, decimal minePercent)
        {

            var generator = new FieldGenerator();
            var field = generator.GenerateField(gameId,horizontal, vertical);

            Mines mines = new Mines();
            mines.Placer(field, minePercent);

            mines.AroundEachCell(field);
            _fieldDao.StoreField(field);

            return field;
        }

        public FieldDto GetField(int gameId)
        {
            return _fieldDao.GetField(gameId);
        }
        public string GetGameStatus(int gameId)
        {
            GameProgress gameProgress = new GameProgress();
            var fieldDto = GetField(gameId);
            var field = ConvertToField(fieldDto);
            string gameProg = gameProgress.GameStatus(field.MineField);
            return gameProg;
        }
        public void RevealCell(int gameId, int horizontal, int vertical)
        {
            var fieldDto = GetField(gameId);
            var field = ConvertToField(fieldDto);
            _cellRevealer.RevealCell(field.MineField[horizontal, vertical], field.MineField);
            return;
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
                    cellDto.AmountOfMinesAroundCell,
                    cellDto.GameId
                );
            }

            return field;

        }

    }
}
