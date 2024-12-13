using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Interfaces;

public interface IRoomRepository
{
    List<Room> Get(string number, string type, int pageNumber, int pageSize);
    Room Get(string number);
    Room GetRoomItem(string number);
    // int Count(string number);
    int Count(string number, string type);
    void Insert(Room room);
    void Update(Room room);
}
