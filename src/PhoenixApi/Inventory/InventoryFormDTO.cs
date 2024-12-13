using System.ComponentModel.DataAnnotations;
using PhoenixApi.Validations;

namespace PhoenixApi.Inventory;

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
