using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhoenixWeb.Services;
using PhoenixWeb.ViewModels.Reservation;

namespace PhoenixWeb.Controllers;

public class ReservationController : Controller
{
    private readonly ReservationService _service;

    public ReservationController(ReservationService service)
    {
        _service = service;
    }

    public IActionResult Index(DateTime bookDate, string roomNumber = "", string guestUsername = "", int pageNumber = 1)
    {
        bookDate = DateTime.Today;
        var viewModel = _service.Get(roomNumber, guestUsername, bookDate, pageNumber);
        return View(viewModel);
    }

    [HttpGet("Reservation/Insert/{number}")]
    public IActionResult Add(string number)
    {
        var viewModel = _service.Get(number);
        viewModel.CheckIn = DateTime.Now;
        viewModel.CheckOut = DateTime.Now;
        return View("Insert", viewModel);
    }

    [HttpPost("Reservation/Insert/{number}")]
    public IActionResult Insert(ReservationInsertViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            viewModel.GuestUsername = username;
            viewModel.BookDate = DateTime.Today;
            viewModel.PaymentDate = DateTime.Today;
            viewModel.ReservationMethod = "OW";
            _service.Insert(viewModel);
            return RedirectToAction("Index", "Booking");
        }
        var vm = _service.Get(viewModel.Room.Number);
        vm.CheckIn = DateTime.Now;
        vm.CheckOut = DateTime.Now;
        return View("Insert", vm);
    }

    [HttpGet("Reservation/Detail/{Code}")]
    public IActionResult ReservationDetail(string code)
    {
        var viewModel = _service.ReservationDetail(code);
        return View("Detail", viewModel);
    }
}
