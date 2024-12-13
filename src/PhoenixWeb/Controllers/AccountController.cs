using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PhoenixWeb.Services;
using PhoenixWeb.ViewModels.Account;

namespace PhoenixWeb.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var viewModel = _service.GetLogin();
        return RedirectToAction("Login", "Account", viewModel);
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        if (User?.Identity?.IsAuthenticated ?? true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        var viewModel = _service.GetLogin();
        return View(viewModel);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(AccountLoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var ticket = await _service.SetLogin(viewModel);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ticket.Principal, ticket.Properties);
                return RedirectToAction("Index", "Dashboard");
            }
            catch (System.Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
        }
        var vm = _service.GetLogin();
        return View(vm);
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}
