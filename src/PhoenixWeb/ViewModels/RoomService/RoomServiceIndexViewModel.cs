namespace PhoenixWeb.ViewModels.RoomService;

public class RoomServiceIndexViewModel
{
    public List<RoomServiceViewModel> RoomServices { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string EmployeeNumber { get; set; }
    public string FullName { get; set; }
}
