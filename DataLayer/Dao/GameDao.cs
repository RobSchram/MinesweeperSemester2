using LogicLayer;
using Minesweeper.Data;
using MySql.Data.MySqlClient;
using LogicLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Dao.GameDao;

namespace DataLayer.Dao
{
    public class GameDao : IGameDao
    {
        private readonly DatabaseConnection _connection;
        public GameDao(DatabaseConnection connection)
        {
            this._connection = connection;
        }
        public int CreateGame(Game game)
        {
            try
            {
                _connection.OpenConnection();
                string query = "INSERT INTO game (user_id, game_status) " + "VALUES (@userId, @GameStatus)";

                using (MySqlCommand cmd = new MySqlCommand(query, _connection.myConnection))
                {
                    cmd.Parameters.AddWithValue("@GameStatus", game.GameStatus);
                    cmd.Parameters.AddWithValue("@userId", game.UserId);

                    cmd.ExecuteNonQuery();
                    game.SetGameId((int)cmd.LastInsertedId);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                _connection.CloseConnection();
            }
            return game.GameId;
        }


        public Game GetGameById(int gameId)
        {
            Game game = null;
            try
            {
                _connection.OpenConnection();
                string query = "SELECT * FROM game WHERE id = @gameId";

                using (MySqlCommand cmd = new MySqlCommand(query, _connection.myConnection))
                {
                    cmd.Parameters.AddWithValue("@gameId", gameId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            game = new Game();


                            game.SetGameId(reader.GetInt32("id"));
                            game.SetUserId(reader.GetInt32("user_id"));
                            game.SetGameProgress(reader.GetString("game_status"));

                        }
                    }
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                _connection.CloseConnection();
            }
            return game;
        }
        public Game GetLastGameFromUser(int userId)
        {
            Game game = null;
            try
            {
                _connection.OpenConnection();
                string query = "SELECT * FROM game WHERE user_id = @userId ORDER BY id DESC LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, _connection.myConnection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            game = new Game();
                            game.SetGameId(reader.GetInt32("id"));
                            game.SetUserId(reader.GetInt32("user_id"));
                            game.SetGameProgress(reader.GetString("game_status"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                _connection.CloseConnection();
            }
            return game;
        }
        public void UpdateGameStatus(int gameId, string newGameStatus)
        {
            try
            {
                _connection.OpenConnection();
                string query = "UPDATE game SET game_status = @newGameStatus WHERE id = @gameId";

                using (MySqlCommand cmd = new MySqlCommand(query, _connection.myConnection))
                {
                    cmd.Parameters.AddWithValue("@newGameStatus", newGameStatus);
                    cmd.Parameters.AddWithValue("@gameId", gameId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

    }

}

