using Microsoft.AspNetCore.Mvc;

namespace PhoenixApi.RoomInventory;

[Route("api/v1/roominventory")]
[ApiController]
public class RoomInventoryController : ControllerBase
{
    private readonly RoomInventoryService _service;

    public RoomInventoryController(RoomInventoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var dto = _service.Get();
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Insert(RoomInventoryFormDTO dto)
    {
        _service.Insert(dto);
        return Ok(new {message = "Success Add Room Inventory"});
    }
}
