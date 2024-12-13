using System.ComponentModel.DataAnnotations;

namespace PhoenixWeb.BackendGateway;

public class RoomServiceFormDTO
{
    [Required]
    [StringLength(maximumLength:20)]
    public string EmployeeNumber { get; set; } = null!;
    [Required]
    [StringLength(maximumLength:50)]
    public string FirstName { get; set; } = null!;
    [StringLength(maximumLength:50)]
    public string? MiddleName { get; set; }
    [StringLength(maximumLength:50)]
    public string? LastName { get; set; }
    [StringLength(maximumLength:50)]
    public string? OutsourcingCompany { get; set; }
}
