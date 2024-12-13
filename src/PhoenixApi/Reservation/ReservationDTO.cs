using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhoenixApi;

public class ReservationDTO
{
    public List<SelectListItem> GetMonth { get; set; }
    public List<SelectListItem> GetYear { get; set; }
}
