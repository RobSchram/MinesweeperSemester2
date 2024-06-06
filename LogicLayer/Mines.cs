namespace LogicLayer
{
    public class Mines
    {
        public void Placer(Cell[,] field, decimal minePercent)
        {
            int horizontal = field.GetLength(0);
            int vertical = field.GetLength(1);
            decimal amountOfMines = Convert.ToInt32(Math.Round(Convert.ToDecimal(horizontal * vertical) * minePercent));

            Random random = new Random();
            for (int i = 0; i < amountOfMines; i++)
            {
                int horizontalIndex = random.Next(0, horizontal);
                int verticalIndex = random.Next(0, vertical);
                if (field[horizontalIndex, verticalIndex].IsMine == 1)
                {
                    i--;
                }
                else
                {
                    field[horizontalIndex, verticalIndex].MakeCellMine();
                }
            }
        }
        public void AroundEachCell(Cell[,] field)
        {
            int horizontal = field.GetLength(0);
            int vertical = field.GetLength(1);
            for (int h = 0; h < horizontal; h++)
            {
                for (int v = 0; v < vertical; v++)
                {
                    int count = 0;
                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {

                            int neighborX = h + xOffset;
                            int neighborY = v + yOffset;

                            if (neighborX >= 0 && neighborX < horizontal && neighborY >= 0 && neighborY < vertical)
                            {
                                Cell neighborCell = field[neighborX, neighborY];
                                if (neighborCell.IsMine == 1) count++;
                            }
                        }
                    }
                    Cell cell = field[h, v];
                    if (cell.IsMine != 1)
                    {
                        cell.SetMinesAroundCell(count);
                    }
                }
            }
        }
    }
}
