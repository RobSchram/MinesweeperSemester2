using LogicLayer;
using LogicLayer.interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        var hashedPassword = _accountService.HashPassword(model.Password);
        if (_accountService.CreateUser(model.Username, hashedPassword)) return RedirectToAction("LogIn", "Account");

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(AccountModel model)
    {

        ApplicationUser user = _accountService.SearchAccount(model.Username);
        if (user != null && _accountService.VerifyPassword(user.Password, model.Password))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, model.Username),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Privacy", "Home");
        }

        // Gebruiker niet gevonden, terug naar inlogpagina
        return View(model);
    }
}

