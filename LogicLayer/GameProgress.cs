namespace LogicLayer
{
    public class GameProgress
    {
        public string GameStatus(Cell[,] field)
        {
            int fieldLength = field.GetLength(0);
            int fieldWithd = field.GetLength(1);
            for (int i = 0; i < fieldLength; i++)
            {
                for (int j = 0; j < fieldWithd; j++)
                {
                    if (field[i, j].IsMine == 1 && field[i, j].IsVisible == 1) return "Lost";

                }
            }
            for (int i = 0; i < fieldLength; i++)
            {
                for (int j = 0; j < fieldWithd; j++)
                {
                    if (field[i, j].IsVisible != 1 && field[i, j].IsMine == 0) return "In progress";
                }
            }
            return "Won";
        }
    }
}
