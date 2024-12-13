using System.ComponentModel.DataAnnotations;

namespace PhoenixWeb.BackendGateway;

public class InventoryFormDTO
{
    [Required]
    // [UniqueNameInventoryValidation]
    [StringLength(maximumLength:50)]
    public string Name { get; set; } = null!;
    [Required]
    public int Stock { get; set; }
    public string? Description { get; set; }
}
