using Microsoft.AspNetCore.Mvc;
using PhoenixWeb.Services;
using PhoenixWeb.ViewModels.Guest;

namespace PhoenixWeb.Controllers;

public class GuestController : Controller
{
    private readonly GuestService _service;
    private readonly AccountService _accountService;

    public GuestController(GuestService service)
    {
        _service = service;
    }

    [HttpGet("Register")]
    public IActionResult Register()
    {
        if (!User?.Identity?.IsAuthenticated ?? true)
        {
            var model = _service.GetRegister();
            return View("Register", model);
        }
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpPost("Register")]
    public IActionResult Register(GuestInsertViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Register(viewModel);
            _accountService.GetLogin();
            return RedirectToAction("Login", "Account");
        }
        var model = _service.GetRegister();
        return View("Register", model);
    }
}