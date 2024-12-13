using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Repositories;

public class RoomInventoryRepository : IRoomInventoryRepository
{
    private readonly PhoenixContext _dbContext;

    public RoomInventoryRepository(PhoenixContext dbContext)
    {
        _dbContext = dbContext;
    }

    public RoomInventory Get(long id)
    {
        return _dbContext.RoomInventories.Find(id);
    }
    public void Insert(RoomInventory roomInventory)
    {
        _dbContext.RoomInventories.Add(roomInventory);
        _dbContext.SaveChanges();
    }

    public void Delete(RoomInventory roomInventory)
    {
        _dbContext.RoomInventories.Remove(roomInventory);
        _dbContext.SaveChanges();
    }
}
