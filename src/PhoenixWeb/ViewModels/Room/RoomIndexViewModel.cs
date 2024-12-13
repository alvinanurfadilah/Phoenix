using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhoenixWeb.ViewModels.Room;

public class RoomIndexViewModel
{
    public List<RoomViewModel> Rooms { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string Number { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }

    public List<SelectListItem> SearchType { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> SearchStatus { get; set; } = new List<SelectListItem>();
}
