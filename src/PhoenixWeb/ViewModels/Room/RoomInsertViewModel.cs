using System.ComponentModel.DataAnnotations;

namespace PhoenixWeb.ViewModels.Room;

public class RoomInsertViewModel
{
    [Required]
    [UniqueNumberRoomValidation]
    [StringLength(maximumLength:10)]
    public string Number { get; set; } = null!;
    [Range(1, Int32.MaxValue, ErrorMessage = "The {0} field is must be greater than 0.")]
    public int Floor { get; set; }
    [Required]
    [StringLength(maximumLength:3)]
    [RegularExpression("^(RS|RD|RT|VS|VD|VT)$", ErrorMessage = "Input must be (RS|RD|RT|VS|VD|VT)")]
    public string RoomType { get; set; } = null!;
    [Range(0, Int32.MaxValue, ErrorMessage = "The {0} field is must be greater than 0.")]
    public int GuestLimit { get; set; }
    [Range(0, Int32.MaxValue, ErrorMessage = "The {0} field is must be greater than 0.")]
    public decimal Cost { get; set; }
    public string? Description { get; set; }
}
