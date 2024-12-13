using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.BackendGateway;

[Route("api/v1/account")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly AccountBackendGatewayService _service;

    public AccountController(AccountBackendGatewayService service)
    {
        _service = service;
    }

    [HttpPut]
    public async Task<IActionResult> ChangePassword(AccountChangePasswordDTO dto)
    {
        var token = User.FindFirst("token")?.Value ?? string.Empty;
        dto.Username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        dto.Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        var response = await _service.ChangePassword(dto, token);
        return Ok(response);
    }
}
