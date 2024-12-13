using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.BackendGateway;

[Route("api/v1/admin")]
[ApiController]
[Authorize]
public class AdminController : ControllerBase
{
    private readonly AdminBackendGatewayService _service;

    public AdminController(AdminBackendGatewayService service)
    {
        _service = service;
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> Get(string username)
    {
        var token = User.FindFirst("token")?.Value??string.Empty;
        var response = await _service.Get(username, token);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(AdminFormDTO dto)
    {
        var token = User.FindFirstValue("Token");
        var response = await _service.Insert(dto, token);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(AdminFormDTO dto)
    {
        var token = User.FindFirst("token")?.Value??string.Empty;
        var response = await _service.Update(dto, token);
        return Ok(response);
    }
}
