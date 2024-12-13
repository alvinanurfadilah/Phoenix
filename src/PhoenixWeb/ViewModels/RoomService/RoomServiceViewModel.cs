using PhoenixDataAccess.Models;

namespace PhoenixWeb.ViewModels.RoomService;

public class RoomServiceViewModel
{
    public string EmployeeNumber { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string? OutsourcingCompany { get; set; }
    public string? MondayRosterStart { get; set; }
    public string? MondayRosterFinish { get; set; }
    public string? TuesdayRosterStart { get; set; }
    public string? TuesdayRosterFinish { get; set; }
    public string? WednesdayRosterStart { get; set; }
    public string? WednesdayRosterFinish { get; set; }
    public string? ThursdayRosterStart { get; set; }
    public string? ThursdayRosterFinish { get; set; }
    public string? FridayRosterStart { get; set; }
    public string? FridayRosterFinish { get; set; }
    public string? SaturdayRosterStart { get; set; }
    public string? SaturdayRosterFinish { get; set; }
    public string? SundayRosterStart { get; set; }
    public string? SundayRosterFinish { get; set; }
}