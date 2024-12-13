using Microsoft.EntityFrameworkCore;
using PhoenixBusiness.Exceptions;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixBusiness;

public class RoomRepository : IRoomRepository
{
    private readonly PhoenixContext _dbContext;

    public RoomRepository(PhoenixContext dbContext)
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

    public Room Get(string number)
    {
        return _dbContext.Rooms.Find(number);
    }

    public Room GetRoomItem(string number)
    {
        return _dbContext.Rooms
        .Include(room => room.RoomInventories)
        .ThenInclude(inv => inv.InventoryNameNavigation)
        .Where(room => room.Number == number)
        .FirstOrDefault();
    }

    public int Count(string number, string type)
    {
        return _dbContext.Rooms
        .Where(room => room.Number.ToLower().Contains(number??"".ToLower()) && (room.RoomType == type || null == type || string.Empty == type))
        .Count();
    }

    public void Insert(Room room)
    {
        _dbContext.Rooms.Add(room);
        _dbContext.SaveChanges();
    }

    public void Update(Room room)
    {
        _dbContext.Rooms.Update(room);
        _dbContext.SaveChanges();
    }
}
