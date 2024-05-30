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
using web.Models;

namespace MinesweeperSemester2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFieldService _fieldService;
        private readonly IStoreData _storeData;
        private readonly IFieldDao _fieldDao;
        private readonly IClearData _clearData;

        public HomeController(ILogger<HomeController> logger,IFieldService fieldService, IStoreData storeData, IFieldDao fieldDao, IClearData clearData)
        {
            _fieldService = fieldService;
            _storeData = storeData;
            _fieldDao = fieldDao;
            _logger = logger;
            _clearData = clearData;
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
            int minePercent = 20;
            int horizontal = 10;
            int vertical = 10;

            var field = _fieldService.GenerateField(horizontal,vertical, minePercent);
            List<CellModel> cellViewModels = new List<CellModel>();

            foreach (var cell in field)
            {
                CellModel cellViewModel = new CellModel
                {
                    Horizontal = cell.horizontal,
                    Vertical = cell.vertical,
                    IsMine = cell.isMine,
                    IsVisible = cell.isVisible,
                    MinesAroundCell = cell.amountOfMinesAroundCell,
                };

                cellViewModels.Add(cellViewModel);
            }

            return View("Index", cellViewModels);
        }



        [HttpPost]
        public IActionResult LoadGame()
        {
            List<CellDto> field = _fieldService.GetField();
            List<CellModel> cellViewModels = new List<CellModel>();

            foreach (CellDto cell in field)
            {
                CellModel cellViewModel = new CellModel
                {
                    Horizontal = cell.Horizontal,
                    Vertical = cell.Vertical,
                    IsMine = cell.IsMine,
                    IsVisible = cell.IsVisible,
                    MinesAroundCell = cell.AmountOfMinesAroundCell,
                };

                cellViewModels.Add(cellViewModel);
            }

            return View("Index", cellViewModels);
        }
        [HttpPost]
        public IActionResult CellClick(int row, int col)
        {
            _fieldService.RevealCell(row, col);
            return LoadGame();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
