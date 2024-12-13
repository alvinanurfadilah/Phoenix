namespace PhoenixWeb.ViewModels.Inventory;

public class InventoryIndexViewModel
{
    public List<InventoryViewModel> Inventories { get; set; }
    public PaginationViewModel Pagination { get; set; }
}
