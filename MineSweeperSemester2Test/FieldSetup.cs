using LogicLayer;

namespace MineSweeperSemester2Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            FieldGenerator fieldGenerator = new FieldGenerator();
            Mines mines = new Mines();
        }

        [Test]
        public void TestGenerateField()
        {
            int horizontal = 10;
            int vertical = 10;
            FieldGenerator fieldGenerator = new FieldGenerator();
            var field = fieldGenerator.GenerateField(5,horizontal, vertical);
            int fieldLength = field.GetLength(0);
            int fieldWidth = field.GetLength(1);
            int fieldSize = fieldLength * fieldWidth;


            Assert.AreEqual(fieldSize, (horizontal * vertical));
        }
        [Test]
        public void MinePLacerTest() 
        {
            int horizontal = 10;
            int vertical = 10;
            FieldGenerator fieldGenerator = new FieldGenerator();
            var field = fieldGenerator.GenerateField(5, horizontal, vertical);
            Mines mines = new Mines();
            decimal minePercent = 0.15m;
            int fieldLength = field.GetLength(0);
            int fieldWidth = field.GetLength(1);
            int fieldSize = fieldLength * fieldWidth;
            int amountOfMines = Convert.ToInt32(Math.Round(fieldSize * minePercent));
            Console.WriteLine(amountOfMines);
            Console.WriteLine(fieldSize);
            Console.WriteLine(fieldLength);
            Console.WriteLine(fieldWidth);
            mines.Placer(field, minePercent);
            int count = 0;
            for (int i = 0; i < horizontal; i++)
            {
                for (int j = 0; j < vertical; j++)
                {
                    if (field[i, j].IsMine == 1)
                    {
                        count++;
                    }

                }
            }
            Assert.AreEqual(amountOfMines, count);
        }
        [Test]
        public void MinesAroundCellCountTest()
        {
            int horizontal = 10;
            int vertical = 10;
            FieldGenerator fieldGenerator = new FieldGenerator();
            var field = fieldGenerator.GenerateField(5, horizontal, vertical);
            Mines mines = new Mines();
            decimal minePercent = 0.15m;
            mines.Placer(field, minePercent);
            int count = 0;
            mines.AroundEachCell(field);
            for (int j = 0; j < horizontal; j++)
            {
                for(int k = 0; k < vertical; k++)
                {
                    if(field[j, k].IsMine != 1)
                    {
                        Console.Write(field[j, k].AmountOfMinesAroundCell + " ");
                    }
                    else
                    {
                        Console.Write( "m ");
                    }
                }
                Console.WriteLine();
            }

            Assert.Pass();
        }
    }
}