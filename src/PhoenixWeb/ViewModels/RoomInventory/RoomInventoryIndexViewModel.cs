namespace PhoenixWeb.ViewModels.RoomInventory;

public class RoomInventoryIndexViewModel
{
    public List<RoomInventoryViewModel> RoomInventories { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string RoomNumber { get; set; } = null!;
    public int Floor { get; set; }
    public string RoomType { get; set; } = null!;
    public int GuestLimit { get; set; }
}
