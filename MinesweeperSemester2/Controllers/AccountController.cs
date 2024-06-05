using LogicLayer.interfaces;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult LogIn()
    {
        var model = new AccountModel();
        return View(model);
    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(AccountModel model)
    {

        if (_accountService.CreateUser(model.Username, model.Password)) return RedirectToAction("LogIn", "Account");

        return View(model);
    }

    [HttpPost]
    public IActionResult LogIn(AccountModel model)
    {

            if (_accountService.SearchAccount(model.Username, model.Password))
            {
                return RedirectToAction("Privacy", "Home");
            }

        return View(model);
    }
}

