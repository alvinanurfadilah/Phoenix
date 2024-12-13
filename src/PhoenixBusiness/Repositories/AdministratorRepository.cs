using Microsoft.Extensions.Logging;
using PhoenixBusiness.Exceptions;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Repositories;

public class AdministratorRepository : IAdministratorRepository
{
    private readonly PhoenixContext _dbContext;

    public AdministratorRepository(PhoenixContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Administrator> Get(int pageNumber, int pageSize)
    {
        return _dbContext.Administrators
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Administrator Get(string username)
    {
        return _dbContext.Administrators.Find(username) ?? throw new LoginException("Username or Password or Role is invalid!");
    }

    public int Count()
    {
        return _dbContext.Administrators.Count();
    }

    public void Insert(Administrator administrator)
    {
        _dbContext.Administrators.Add(administrator);
        _dbContext.SaveChanges();
    }

    public void Update(Administrator administrator)
    {
        _dbContext.Administrators.Update(administrator);
        _dbContext.SaveChanges();
    }

    public void ChangePassword(Administrator administrator)
    {
        _dbContext.Administrators.Update(administrator);
        _dbContext.SaveChanges();
    }
}
