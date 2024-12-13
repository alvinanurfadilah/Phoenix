using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhoenixApi.RoomInventory;

public class RoomInventoryFormDTO
{
    public string RoomNumber { get; set; } = null!;
    public string InventoryName { get; set; } = null!;
    public int Quantity { get; set; }

    public List<SelectListItem>? Inventories { get; set; }
}
