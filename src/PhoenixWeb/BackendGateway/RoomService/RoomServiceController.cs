using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.BackendGateway;

[Route("api/v1/roomservice")]
[ApiController]
[Authorize]
public class RoomServiceController : ControllerBase
{
    private readonly RoomServiceBackendGatewayService _service;

    public RoomServiceController(RoomServiceBackendGatewayService service)
    {
        _service = service;
    }

    [HttpGet("{employeeNumber}")]
    public async Task<IActionResult> Get(string employeeNumber)
    {
        var token = User.FindFirst("token")?.Value ?? string.Empty;
        var response = await _service.Get(employeeNumber, token);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(RoomServiceFormDTO dto)
    {
        var token = User.FindFirst("token")?.Value ?? string.Empty;
        var response = await _service.Insert(dto, token);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(RoomServiceFormDTO dto)
    {
        var token = User.FindFirst("token")?.Value ?? string.Empty;
        var response = await _service.Update(dto, token);
        return Ok(response);
    }
}
