using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoenixApi.Admin;

[Route("api/v1/admin")]
[ApiController]
[Authorize]
public class AdministratorController : ControllerBase
{
    private readonly AdministratorService _service;

    public AdministratorController(AdministratorService service)
    {
        _service = service;
    }

    [HttpGet("{username}")]
    public IActionResult Get(string username)
    {
        var dto = _service.Get(username);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Insert(AdminFormDTO dto)
    {
        return Ok(_service.Insert(dto));
    }

    [HttpPut]
    public IActionResult Update(AdminFormDTO dto)
    {
        _service.Update(dto);
        return Ok(new {message = "Success Update Admin"});
    }
}
