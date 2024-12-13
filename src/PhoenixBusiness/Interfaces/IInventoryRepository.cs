using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Interfaces;

public interface IInventoryRepository
{
    List<Inventory> Get(int pageNumber, int pageSize);
    List<Inventory> Get();
    int Count();
    Inventory Get(string name);
    void Insert(Inventory inventory);
    void Update(Inventory inventory);
    void Delete(Inventory inventory);
}
