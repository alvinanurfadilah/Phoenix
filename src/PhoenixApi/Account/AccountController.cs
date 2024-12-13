using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace PhoenixApi.Account;

[Route("api/v1/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Login(AccountRequestDTO request)
    {
        try
        {
            var response = _service.GetToken(request);
            return Ok(response);
        }
        catch (System.Exception exception)
        {
            return Unauthorized(exception.Message);
        }
    }

    [HttpPut]
    public IActionResult ChangePassword(AccountChangePasswordDTO dto)
    {
        dto.Username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        _service.ChangePassword(dto);
        dto.Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        return Ok( new {message = "Success Change Password"});
    }
}
