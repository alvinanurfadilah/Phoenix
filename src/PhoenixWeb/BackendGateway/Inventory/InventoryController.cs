using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.BackendGateway;

[Route("api/v1/inventory")]
[ApiController]
[Authorize]
public class InventoryController : ControllerBase
{
    private readonly InventoryBackendGatewayService _service;

    public InventoryController(InventoryBackendGatewayService service)
    {
        _service = service;
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> Get(string name)
    {
        var token = User.FindFirst("token")?.Value ?? string.Empty;
        var response = await _service.Get(name, token);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(InventoryFormDTO dto)
    {
        var token = User.FindFirst("token")?.Value ?? string.Empty;
        var response = await _service.Insert(dto, token);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(InventoryFormDTO dto)
    {
        var token = User.FindFirst("token")?.Value ?? string.Empty;
        var response = await _service.Update(dto, token);
        return Ok(response);
    }
}
