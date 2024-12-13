using Microsoft.AspNetCore.Mvc;

namespace PhoenixApi.Reservation;

[Route("api/v1/reservation")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly ReservationService _service;

    public ReservationController(ReservationService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var dto = _service.Get();
        return Ok(dto);
    }

    [HttpGet("totalincome/{month}/{year}")]
    public IActionResult TotalIncome(int month, int year)
    {
        var dto = _service.TotalIncome(month, year);
        return Ok(dto);
    }
}
