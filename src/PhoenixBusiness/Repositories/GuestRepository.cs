using PhoenixBusiness.Exceptions;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Repositories;

public class GuestRepository : IGuestRepository
{
    private readonly PhoenixContext _dbContext;

    public GuestRepository(PhoenixContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guest Get(string username)
    {
        return _dbContext.Guests.Find(username) ?? throw new LoginException("Username or Password or Role is invalid!");
    }

    public void Insert(Guest guest)
    {
        _dbContext.Add(guest);
        _dbContext.SaveChanges();
    }

    public void ChangePassword(Guest guest)
    {
        _dbContext.Guests.Update(guest);
        _dbContext.SaveChanges();
    }
}
