using LogicLayer;

namespace LogicLayer
{
    public class Game
    {
        public int fieldSize;
        public int minePercent;
        public Game()
        {
            Field field = new Field(minePercent ,10,10);
        }
        public void MarkCell()
        {

        }


    }
}
