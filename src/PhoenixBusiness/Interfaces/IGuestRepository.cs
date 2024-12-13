using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Interfaces;

public interface IGuestRepository
{
    Guest Get(string username);
    void Insert(Guest guest);
    void ChangePassword(Guest guest);
}
