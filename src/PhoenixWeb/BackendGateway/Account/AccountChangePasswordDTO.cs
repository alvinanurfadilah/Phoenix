using System.ComponentModel.DataAnnotations;

namespace PhoenixWeb.BackendGateway;

public class AccountChangePasswordDTO
{
    public string? Role { get; set; }
    public string? Username { get; set; }
    [Required]
    public string OldPassword { get; set; } = null!;
    [Required]
    public string NewPassword { get; set; } = null!;
    [Compare("NewPassword", ErrorMessage = "Confirm Password do not match!")]
    public string ConfirmPassword { get; set; }
}
