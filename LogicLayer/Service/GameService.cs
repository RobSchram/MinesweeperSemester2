using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.interfaces;

namespace LogicLayer.Service
{
    public class GameService : IGameService
    {
        private readonly IGameDao _gameDao;
        private readonly IFieldService _fieldService;
        
        
        public GameService(IGameDao gameDao, IFieldService fieldService)
        {
            _gameDao = gameDao;
            _fieldService = fieldService;
        }
        public int CreateGame(Game game)
        {
            return _gameDao.CreateGame(game);
        }
        public Game GetGame(int gameId)
        {
            return _gameDao.GetGameById(gameId);
        }
        public Game UpdateGameStatus(Game game) 
        {
            string gamestatus = _fieldService.GetGameStatus(game.GameId);
            game.SetGameProgress( gamestatus);
            _gameDao.UpdateGameStatus(game.GameId, gamestatus);
            return game;
        }
        public Game GetLastGameFromUser(int userId)
        {
            return _gameDao.GetLastGameFromUser(userId);
        }



    }
}
