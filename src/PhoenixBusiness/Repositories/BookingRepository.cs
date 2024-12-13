using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixBusiness;

public class BookingRepository : IBookingRepository
{
    private readonly PhoenixContext _dbContext;

    public BookingRepository(PhoenixContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Room> Get(string number, string type, int pageNumber, int pageSize)
    {
        return _dbContext.Rooms
        .Where(room => room.Number.ToLower().Contains(number??"".ToLower()) && (room.RoomType == type || null == type || string.Empty == type))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int Count(string number, string type)
    {
        return _dbContext.Rooms
        .Where(room => room.Number.ToLower().Contains(number??"".ToLower()) && (room.RoomType == type || null == type || string.Empty == type))
        .Count();
    }
}
