using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Interfaces;

public interface IAdministratorRepository
{
    List<Administrator> Get(int pageNumber, int pageSize);
    Administrator Get(string username);
    int Count();
    void Insert(Administrator administrator);
    void Update(Administrator administrator);
    void ChangePassword(Administrator administrator);
}
