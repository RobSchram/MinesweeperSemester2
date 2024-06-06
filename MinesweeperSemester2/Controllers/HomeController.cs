using DataLayer;
using DataLayer.Dao;
using LogicLayer;
using LogicLayer.Dto;
using LogicLayer.interfaces;
using LogicLayer.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MinesweeperSemester2.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using web.Models;

namespace MinesweeperSemester2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFieldService _fieldService;
        private readonly IGameService _gameService;

        public HomeController(ILogger<HomeController> logger, IFieldService fieldService, IGameService gameService)
        {
            _fieldService = fieldService;
            _logger = logger;
            _gameService = gameService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StartGame( int horizontal, int vertical, decimal minePercent)
        {
            minePercent = minePercent / 100;
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Game game = new Game();
            game.SetUserId(userId);
            game.SetGameProgress("In progress");
            int gameId = _gameService.CreateGame(game);

            var field = _fieldService.GenerateField(gameId, horizontal, vertical, minePercent);
            FieldModel fieldModel = new FieldModel
            {
                field = new CellModel[horizontal, vertical],
                GameStatus = game.GameStatus
            };


            foreach (var cell in field)
            {
                CellModel cellViewModel = new CellModel
                {
                    GameId = cell.GameId,
                    Horizontal = cell.Horizontal,
                    Vertical = cell.Vertical,
                    IsMine = cell.IsMine,
                    IsVisible = cell.IsVisible,
                    MinesAroundCell = cell.AmountOfMinesAroundCell,
                };
                if (cell.Horizontal >= 0 && cell.Horizontal < horizontal &&
                        cell.Vertical >= 0 && cell.Vertical < vertical)
                {
                    fieldModel.field[cell.Horizontal, cell.Vertical] = cellViewModel;
                }
            }
            return View("Privacy", fieldModel);
        }



        [HttpPost]
        public IActionResult LoadGame()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var game = _gameService.GetLastGameFromUser(userId);
            if (game != null)
            {
                FieldDto field = _fieldService.GetField(game.GameId);


                FieldModel mineField = new FieldModel
                {
                    field = new CellModel[field.MineField.GetLength(0), field.MineField.GetLength(1)],
                };

                foreach (CellDto cell in field.MineField)
                {
                    CellModel cellViewModel = new CellModel
                    {
                        Horizontal = cell.Horizontal,
                        Vertical = cell.Vertical,
                        IsMine = cell.IsMine,
                        IsVisible = cell.IsVisible,
                        MinesAroundCell = cell.AmountOfMinesAroundCell,
                    };

                    mineField.field[cell.Horizontal, cell.Vertical] = cellViewModel;
                }
                mineField.GameStatus = game.GameStatus;

                return View("Privacy", mineField);
            }
            return View("Privacy");
        }

        [HttpPost]
        public IActionResult CellClick(int horizontal, int vertical)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var game = _gameService.GetLastGameFromUser(userId);
            _fieldService.RevealCell(game.GameId, horizontal, vertical);
            _gameService.UpdateGameStatus(game);
            return LoadGame();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
