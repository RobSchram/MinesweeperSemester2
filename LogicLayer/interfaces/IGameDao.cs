namespace LogicLayer.interfaces
{
    public interface IGameDao
    {
        public int CreateGame(Game game);
        public Game GetGameById(int gameId);
        public Game GetLastGameFromUser(int userId);
        public void UpdateGameStatus(int gameId, string newGameStatus);
    }
}
