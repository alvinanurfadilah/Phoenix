using Microsoft.AspNetCore.Mvc;
using PhoenixWeb.Services;
using PhoenixWeb.ViewModels.RoomService;

namespace PhoenixWeb.Controllers;

public class RoomServiceController : Controller
{
    private readonly RoomServiceService _service;

    public RoomServiceController(RoomServiceService service)
    {
        _service = service;
    }

    public IActionResult Index(string employeeNumber, string fullName, int pageNumber = 1)
    {
        var viewModel = _service.Get(employeeNumber, fullName, pageNumber);
        return View(viewModel);
    }

    [HttpGet("RoomService/Roster/{employeeNumber}")]
    public IActionResult Roster(string employeeNumber)
    {
        var viewModel = _service.Get(employeeNumber);
        return View("Roster", viewModel);
    }

    [HttpGet("Roster/Update/{employeeNumber}")]
    public IActionResult RosterEdit(string employeeNumber)
    {
        var viewModel = _service.GetRoster(employeeNumber);
        return View("RosterUpdate", viewModel);
    }

    [HttpPost("Roster/Update/{employeeNumber}")]
    public IActionResult RosterUpdate(RoomServiceRosterUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Update(viewModel);
            return RedirectToAction("Roster", new {employeeNumber = viewModel.EmployeeNumber});
        }
        var vm = _service.GetRoster(viewModel.EmployeeNumber);
        return View("RosterUpdate", vm);
    }
}
