using System.ComponentModel.DataAnnotations;

namespace PhoenixWeb.BackendGateway;

public class AdminFormDTO
{
    [Required]
    // [UniqueUsernameAdminValidation]
    [StringLength(maximumLength:20)]
    public string? Username { get; set; }
    public string? Password { get; set; }
    [Required]
    [StringLength(maximumLength:50)]
    public string JobTitle { get; set; }
}
