using PhoenixWeb.ViewModels.RoomInventory;

namespace PhoenixWeb.ViewModels.Room;

public class RoomInventoryItemViewModel
{
    public string RoomNumber { get; set; } = null!;
    public int Floor { get; set; }
    public string RoomType { get; set; } = null!;
    public int GuestLimit { get; set; }
    public List<RoomInventoryViewModel> RoomInventories { get; set; } = new List<RoomInventoryViewModel>();
    public PaginationViewModel Pagination { get; set; }
}
