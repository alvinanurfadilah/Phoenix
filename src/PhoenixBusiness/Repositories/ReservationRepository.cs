using Microsoft.EntityFrameworkCore;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly PhoenixContext _dbContext;

    public ReservationRepository(PhoenixContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Reservation> Get(string roomNumber, string guestUsername, DateTime bookDate, int pageNumber, int pageSize)
    {
        return _dbContext.Reservations
        .Where(res => res.RoomNumber.ToLower().Contains(roomNumber??"".ToLower()) && res.GuestUsername.ToLower().Contains(guestUsername??"".ToLower()) && res.BookDate <= bookDate)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Reservation Get(string code)
    {
        return _dbContext.Reservations
        .Include(res => res.GuestUsernameNavigation)
        .Include(res => res.RoomNumberNavigation)
        .Where(res => res.Code == code)
        .FirstOrDefault();
    }

    public int Count(string roomNumber, string guestUsername, DateTime bookDate)
    {
        return _dbContext.Reservations
        .Where(res => res.RoomNumber.ToLower().Contains(roomNumber??"".ToLower()) && res.GuestUsername.ToLower().Contains(guestUsername??"".ToLower()) && res.BookDate <= bookDate)
        .Count();
    }

    public void Insert(Reservation reservation)
    {
        _dbContext.Reservations.Add(reservation);
        _dbContext.SaveChanges();
    }

    public List<int> GetYear()
    {
        return _dbContext.Reservations
        .Select(year => year.PaymentDate.Value.Year)
        .Distinct()
        .ToList();
    }

    public decimal TotalIncome(int month, int year)
    {
        return _dbContext.Reservations
        .Where(res => res.PaymentDate.Value.Month == month && res.PaymentDate.Value.Year == year)
        .Sum(res => res.Cost);
    }

    public List<Reservation> Get(string roomNumber, string guestUsername)
    {
        return _dbContext.Reservations
        .Include(res => res.GuestUsernameNavigation)
        .Include(res => res.RoomNumberNavigation)
        .Where(res => res.RoomNumber == roomNumber && res.GuestUsername == guestUsername).ToList();
    }
}
