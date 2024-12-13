using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixWeb.ViewModels;
using PhoenixWeb.ViewModels.Booking;

namespace PhoenixWeb;

public class BookingIndexViewModel
{
    public List<BookingViewModel> Bookings { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string Number { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }

    public List<SelectListItem> SearchType { get; set; }
}
