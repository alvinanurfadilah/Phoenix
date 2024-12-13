using Microsoft.AspNetCore.Mvc;
using PhoenixWeb.Services;

namespace PhoenixWeb.Controllers;

public class BookingController : Controller
{
    private readonly BookingService _service;

    public BookingController(BookingService service)
    {
        _service = service;
    }

    public IActionResult Index(string number = "", string type = "", int pageNumber = 1)
    {
        var viewModel = _service.Get(number, type, pageNumber);
        return View(viewModel);
    }

    [HttpGet("Booking/Detail/{number}")]
    public IActionResult Detail(string number = "")
    {
        var viewModel = _service.Get(number);
        return View("Detail", viewModel);
    }
}
