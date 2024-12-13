using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixWeb.ViewModels.Room;

namespace PhoenixWeb.ViewModels.Reservation;

public class ReservationInsertViewModel
{
    public string Code { get; set; } = null!;
    public string? ReservationMethod { get; set; }
    public RoomViewModel? Room { get; set; }
    public string? RoomNumber { get; set; }
    public string? GuestUsername { get; set; }
    public DateTime? BookDate { get; set; }
    [Required]
    public DateTime CheckIn { get; set; }
    [Required]
    public DateTime CheckOut { get; set; }
    public decimal Cost { get; set; }
    public DateTime? PaymentDate { get; set; }
    [Required]
    public string PaymentMethod { get; set; } = null!;
    public string? Remark { get; set; }

    public List<SelectListItem>? PaymentMethods { get; set; } = new List<SelectListItem>();
}