using LogicLayer;
using Microsoft.AspNetCore.Mvc;
using MinesweeperSemester2.Models;

namespace YourNamespace.Controllers
{
    public class FieldController : Controller
    {
        [HttpGet]
        public IActionResult GenerateField(int horizontal, int vertical, int minePercent)
        {
            var field = new Field(horizontal, vertical, minePercent);
            var generatedField = field.Fieldgenerator();

            FieldViewModel fieldViewModel = new FieldViewModel {fieldview = generatedField};
            return View(fieldViewModel);
        }
        [HttpPost]
        public IActionResult UpdateField(FieldViewModel fieldView)
        {
            return View(fieldView);
        }
    }
}
