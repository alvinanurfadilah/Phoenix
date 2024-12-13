using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Interfaces;

public interface IRoomInventoryRepository
{
    RoomInventory Get(long id);
    void Insert(RoomInventory roomInventory);
    void Delete(RoomInventory roomInventory);
}