using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Game
    {
        public int GameId {  get; private set; }
        public int UserId { get; private set; }
        public string? GameStatus { get; private set; }
        public void SetGameProgress(string gameStatus)
        {
            GameStatus = gameStatus;
        }
        public void SetUserId(int userId) 
        {
            UserId = userId;
        }
        public void SetGameId(int gameId)
        {
            GameId = gameId;
        }


    }
}
