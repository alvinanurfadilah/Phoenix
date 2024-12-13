using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View("Index");
    }
}
