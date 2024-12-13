using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Interfaces;

public interface IBookingRepository
{
    List<Room> Get(string number, string type, int pageNumber, int pageSize);
    int Count(string number, string type);
}
