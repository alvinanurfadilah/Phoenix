using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Interfaces;

public interface IRoomServiceRepository
{
    List<RoomService> Get(string employeeNumber, string fullName, int pageNumber, int pageSize);
    RoomService Get(string employeeNumber);
    int Count(string employeeNumber, string fullName);
    void Insert(RoomService roomService);
    void Update(RoomService roomService);
}
