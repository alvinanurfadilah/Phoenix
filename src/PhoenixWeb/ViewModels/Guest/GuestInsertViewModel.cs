using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixWeb.Validations;

namespace PhoenixWeb.ViewModels.Guest;

public class GuestInsertViewModel
{
    [Required]
    [UniqueUsernameGuestValidation]
    [StringLength(maximumLength:20)]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Compare("Password", ErrorMessage = "Confirm Password do not match!")]
    public string ConfirmPassword { get; set; } = null!;
    [Required]
    [StringLength(maximumLength:50)]
    public string FirstName { get; set; } = null!;
    [StringLength(maximumLength:50)]
    public string? MiddleName { get; set; }
    [StringLength(maximumLength:50)]
    public string? LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public string Gender { get; set; } = null!;
    [Required]
    [StringLength(maximumLength:50)]
    public string Citizenship { get; set; } = null!;
    [Required]
    [UniqueIdNumberGuestValidation]
    [StringLength(maximumLength:50)]
    public string IdNumber { get; set; } = null!;

    public List<SelectListItem> Genders { get; set; } = new List<SelectListItem>();
}
