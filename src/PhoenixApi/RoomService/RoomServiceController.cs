using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoenixApi.RoomService;

[Route("api/v1/roomservice")]
[ApiController]
[Authorize]
public class RoomServiceController : ControllerBase
{
    private readonly RoomServiceService _service;

    public RoomServiceController(RoomServiceService service)
    {
        _service = service;
    }

    [HttpGet("{employeeNumber}")]
    public IActionResult Get(string employeeNumber)
    {
        var dto = _service.Get(employeeNumber);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Insert(RoomServiceFormDTO dto)
    {
        _service.Insert(dto);
        return Ok(new {message = "Success Add Room Service"});
    }

    [HttpPut]
    public IActionResult Update(RoomServiceFormDTO dto)
    {
        _service.Update(dto);
        return Ok(new {message = "Success Update Room Service"});
    }
}
