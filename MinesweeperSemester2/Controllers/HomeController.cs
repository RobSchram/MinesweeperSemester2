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
using web.Models;

namespace MinesweeperSemester2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFieldService _fieldService;

        public HomeController(ILogger<HomeController> logger,IFieldService fieldService)
        {
            _fieldService = fieldService;
            _logger = logger;
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
        public IActionResult StartGame()
        {
            decimal minePercent = 0.15m;
            int horizontal = 10;
            int vertical = 10;

            var field = _fieldService.GenerateField(horizontal,vertical, minePercent);
            FieldModel fieldModel = new FieldModel
            {
                field = new CellModel[horizontal, vertical],
                GameStatus = "In progress"
            };

            foreach (var cell in field)
            {
                CellModel cellViewModel = new CellModel
                {
                    Horizontal = cell.Horizontal,
                    Vertical = cell.Vertical,
                    IsMine = cell.IsMine,
                    IsVisible = cell.IsVisible,
                    MinesAroundCell = cell.AmountOfMinesAroundCell,
                };

                fieldModel.field[cell.Horizontal, cell.Vertical] = cellViewModel;
            }
            return View("Index", fieldModel);
        }



        [HttpPost]
        public IActionResult LoadGame(string gameStatus)
        {
            if (gameStatus == null)
            {
                gameStatus = "In progress";
            }
            FieldDto field = _fieldService.GetField();
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
            mineField.GameStatus = gameStatus;
            return View("Index", mineField);
        }
        [HttpPost]
        public IActionResult CellClick(int row, int col)
        {
            string gameStatus =_fieldService.RevealCell(col, row);
            return LoadGame(gameStatus);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
