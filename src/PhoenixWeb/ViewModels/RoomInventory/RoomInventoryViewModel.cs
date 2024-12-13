namespace PhoenixWeb.ViewModels.RoomInventory;

public class RoomInventoryViewModel
{   
    public long Id { get; set; }
    public string InventoryName { get; set; } = null!;
    public int Stock { get; set; }
    public int Quantity { get; set; }

    public string RoomNUmber { get; set; }
}
