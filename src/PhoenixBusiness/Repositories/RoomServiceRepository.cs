using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Repositories;

public class RoomServiceRepository : IRoomServiceRepository
{
    private readonly PhoenixContext _dbContext;

    public RoomServiceRepository(PhoenixContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<RoomService> Get(string employeeNumber, string fullName, int pageNumber, int pageSize)
    {
        return _dbContext.RoomServices
        .Where(service => service.EmployeeNumber.ToLower().Contains(employeeNumber??"".ToLower()) && (service.FirstName + " " + service.MiddleName + " " + service.LastName).ToLower().Contains(fullName??"".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public RoomService Get(string employeeNumber)
    {
        return _dbContext.RoomServices.Find(employeeNumber);
    }

    public int Count(string employeeNumber, string fullName)
    {
        return _dbContext.RoomServices
        .Where(service => service.EmployeeNumber.ToLower().Contains(employeeNumber??"".ToLower()) && (service.FirstName + " " + service.MiddleName + " " + service.LastName).ToLower().Contains(fullName??"".ToLower()))
        .Count();
    }

    public void Insert(RoomService roomService)
    {
        _dbContext.RoomServices.Add(roomService);
        _dbContext.SaveChanges();
    }

    public void Update(RoomService roomService)
    {
        _dbContext.RoomServices.Update(roomService);
        _dbContext.SaveChanges();
    }
}
