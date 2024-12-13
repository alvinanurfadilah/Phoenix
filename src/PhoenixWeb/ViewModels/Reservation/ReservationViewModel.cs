namespace PhoenixWeb.ViewModels.Reservation;

public class ReservationViewModel
{
    public string Code { get; set; } = null!;
    public string RoomNumber { get; set; } = null!;
    public string GuestUsername { get; set; } = null!;
    public string BookDate { get; set; }
    public string CheckIn { get; set; }
    public string CheckOut { get; set; }
    public string? PaymentDate { get; set; }

    public string? ReservationMethod { get; set; }
    public int? Floor { get; set; }
    public string? RoomType { get; set; }
    public string? GuestFullName { get; set; }
    public decimal? Cost { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Remark { get; set; }
}
