namespace PhoenixWeb.ViewModels.Reservation;

public class ReservationIndexViewModel
{
    public List<ReservationViewModel> Reservations { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string RoomNumber { get; set; }
    public string GuestUsername { get; set; }
    public DateTime BookDate { get; set; }
}