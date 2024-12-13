using Microsoft.AspNetCore.Mvc;
using PhoenixWeb.Services;
using PhoenixWeb.ViewModels.Room;

namespace PhoenixWeb.Controllers;

public class RoomController : Controller
{
    private readonly RoomService _service;

    public RoomController(RoomService service)
    {
        _service = service;
    }

    public IActionResult Index(string number = "", string type = "", int pageNumber = 1)
    {
        var viewModel = _service.Get(number, type, pageNumber);
        return View(viewModel);
    }

    [HttpGet("Room/Insert")]
    public IActionResult Add()
    {
        return View("Insert");
    }

    [HttpPost("Room/Insert")]
    public IActionResult Insert(RoomInsertViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Insert(viewModel);
            return RedirectToAction("Index", "Room");
        }

        return View("Insert");
    }

    [HttpGet("Room/Update/{number}")]
    public IActionResult Edit(string number)
    {
        var viewModel = _service.Get(number);
        return View("Update", viewModel);
    }

    [HttpPost("Room/Update/{number}")]
    public IActionResult Update(RoomUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Update(viewModel);
            return RedirectToAction("Index");
        }
        return View("Update");
    }

    [HttpGet("Room/Item/{number}")]
    public IActionResult GetRoomItem(string number)
    {
        var viewModel = _service.GetRoomItem(number);
        return View("RoomItem", viewModel);
    }

    [HttpGet("RoomInventory/delete/{id}")]
    public IActionResult DeleteRoomInventory(long id)
    {
        try
        {
            var getRoomNumber = _service.GetRoomInventory(id);
            _service.DeleteRoomInventory(id);
            return RedirectToAction("GetRoomItem", new {number = getRoomNumber.RoomNUmber});
        }
        catch (System.Exception)
        {
            return View("Delete");
        }
    }

    [HttpGet("MyRoom/{roomNumber}/{guestUsername}")]
    public IActionResult GetMyRoom(string roomNumber, string guestUsername)
    {
        var viewModel = _service.Get(roomNumber, guestUsername);
        return View("MyRoom", viewModel);
    }
}
