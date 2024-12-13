using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly PhoenixContext _dbContext;

    public InventoryRepository(PhoenixContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Inventory> Get(int pageNumber, int pageSize)
    {
        return _dbContext.Inventories
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public List<Inventory> Get()
    {
        return _dbContext.Inventories.ToList();
    }

    public Inventory Get(string name)
    {
        return _dbContext.Inventories.Find(name);
    }

    public int Count()
    {
        return _dbContext.Inventories.Count();
    }

    public void Insert(Inventory inventory)
    {
        _dbContext.Inventories.Add(inventory);
        _dbContext.SaveChanges();
    }

    public void Update(Inventory inventory)
    {
        _dbContext.Inventories.Update(inventory);
        _dbContext.SaveChanges();
    }

    public void Delete(Inventory inventory)
    {
        _dbContext.Inventories.Remove(inventory);
        _dbContext.SaveChanges();
    }
}
