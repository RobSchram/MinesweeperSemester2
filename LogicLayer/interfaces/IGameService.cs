namespace LogicLayer.interfaces
{
    public interface IGameService
    {
        public int CreateGame(Game game);
        public Game GetGame(int gameId);
        public Game UpdateGameStatus(Game game);
        public Game GetLastGameFromUser(int userId);
    }
}
