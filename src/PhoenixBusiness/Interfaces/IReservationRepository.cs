using PhoenixDataAccess.Models;

namespace PhoenixBusiness.Interfaces;

public interface IReservationRepository
{
    List<Reservation> Get(string roomNumber, string guestUsername, DateTime bookDate, int pageNumber, int pageSize);
    Reservation Get(string code);
    int Count(string roomNumber, string guestUsername, DateTime bookDate);
    void Insert(Reservation reservation);
    List<int> GetYear();
    decimal TotalIncome(int month, int year);

    List<Reservation> Get(string roomNumber, string guestUsername);
}
