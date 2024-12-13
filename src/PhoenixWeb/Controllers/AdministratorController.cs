using Microsoft.AspNetCore.Mvc;
using PhoenixWeb.Services;

namespace PhoenixWeb.Controllers;

public class Administrator : Controller
{
    private readonly AdministratorService _service;

    public Administrator(AdministratorService service)
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1)
    {
        var viewModel = _service.Get(pageNumber);
        return View(viewModel);
    }
}
