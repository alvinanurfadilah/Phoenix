using Microsoft.AspNetCore.Mvc;
using PhoenixWeb.Services;

namespace PhoenixWeb.Controllers;

public class InventoryController : Controller
{
    private readonly InventoryService _service;

    public InventoryController(InventoryService service)
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber = 1)
    {
        var viewModel = _service.Get(pageNumber);
        return View(viewModel);
    }

    [HttpGet("inventory/delete/{name}")]
    public IActionResult Delete(string name)
    {
        try
        {
            _service.Delete(name);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return View("Delete");
        }
    }
}
