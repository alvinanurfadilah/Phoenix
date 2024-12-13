using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.BackendGateway;

[Route("api/v1/reservation")]
[ApiController]
[Authorize]
public class ReservationController : ControllerBase
{
    private readonly ReservationBackendGatewayService _service;

    public ReservationController(ReservationBackendGatewayService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var token = User.FindFirst("token")?.Value ?? string.Empty;
        var response = await _service.Get(token);

        return Ok(response);
    }

    [HttpGet("totalincome/{month}/{year}")]
    public async Task<IActionResult> TotalIncome(int month, int year)
    {
        var token = User.FindFirst("token")?.Value ?? string.Empty;
        var response = await _service.TotalIncome(month, year, token);
        return Ok(response);
    }
}
