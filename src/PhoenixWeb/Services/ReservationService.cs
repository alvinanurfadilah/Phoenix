using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;
using PhoenixWeb.ViewModels.Reservation;
using static PhoenixWeb.ViewModels.Constant;

namespace PhoenixWeb.Services;

public class ReservationService
{
    private readonly IReservationRepository _repository;
    private readonly IRoomRepository _roomRepository;

    public ReservationService(IReservationRepository repository, IRoomRepository roomRepository)
    {
        _repository = repository;
        _roomRepository = roomRepository;
    }

    public ReservationIndexViewModel Get(string roomNumber, string guestUsername, DateTime bookDate, int pageNumber)
    {
        var model = _repository.Get(roomNumber, guestUsername, bookDate, pageNumber, PageSize)
        .Select(res => new ReservationViewModel()
        {
            Code = res.Code,
            RoomNumber = res.RoomNumber,
            GuestUsername = res.GuestUsername,
            BookDate = res.BookDate.ToString("dd/MM/yyyy"),
            CheckIn = res.CheckIn.ToString("dd/MM/yyyy"),
            CheckOut = res.CheckOut.ToString("dd/MM/yyyy"),
            PaymentDate = res.PaymentDate?.ToString("dd/MM/yyyy")
        });

        return new ReservationIndexViewModel()
        {
            Reservations = model.ToList(),
            Pagination = new ViewModels.PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count(roomNumber, guestUsername, bookDate)
            },
            RoomNumber = roomNumber,
            GuestUsername = guestUsername,
            BookDate = bookDate
        };
    }

    private List<SelectListItem> GetPaymentMethod()
    {
        List<SelectListItem> paymentMethod = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = "CC",
                Text = "Credit Card"
            },
            new SelectListItem()
            {
                Value = "DD",
                Text = "Direct Debit"
            },
            new SelectListItem()
            {
                Value = "VO",
                Text = "Voucher"
            },
            new SelectListItem()
            {
                Value = "EM",
                Text = "Electronic Money"
            },
            new SelectListItem()
            {
                Value = "CA",
                Text = "Cash"
            }
        };

        return paymentMethod;
    }

    private static int RollingNumber = 1;
    private string GetCodeReservation(string roomNumber)
    {
        DateTime today = DateTime.Today;
        string code = $"{roomNumber}-{today.ToString("dd-MM-yy")}-00{RollingNumber}";
        RollingNumber++;
        return code;
    }

    private decimal GetTotalCost(DateTime checkIn, DateTime checkOut, decimal cost)
    {
        int days = (checkOut - checkIn).Days;

        if (days == 0)
        {
            days = 1;
        } 

        decimal totalCost = days * cost;

        return totalCost;
    }

    public ReservationInsertViewModel Get(string roomNumber)
    {
        var modelRoom = _roomRepository.Get(roomNumber);
        // var modelReservation = _repository.Get(code);
        return new ReservationInsertViewModel()
        {
            Room = new ViewModels.Room.RoomViewModel()
            {
                Number = modelRoom.Number,
                Floor = modelRoom.Floor,
                RoomType = modelRoom.RoomType,
                GuestLimit = modelRoom.GuestLimit,
                Cost = (Int32)modelRoom.Cost
            },
            PaymentMethods = GetPaymentMethod(),
            Code = GetCodeReservation(modelRoom.Number),
            Cost = modelRoom.Cost
        };
    }
    public void Insert(ReservationInsertViewModel viewModel)
    {
        var model = new Reservation()
        {
            Code = GetCodeReservation(viewModel.RoomNumber),
            ReservationMethod = "OW",
            RoomNumber = viewModel.RoomNumber,
            GuestUsername = viewModel.GuestUsername,
            BookDate = DateTime.Today,
            CheckIn = viewModel.CheckIn,
            CheckOut = viewModel.CheckOut,
            Cost = viewModel.Cost,
            PaymentDate = DateTime.Today,
            PaymentMethod = viewModel.PaymentMethod,
            Remark = viewModel.Remark
        };

        _repository.Insert(model);
    }

    public ReservationViewModel ReservationDetail(string code)
    {
        var model = _repository.Get(code);
        return new ReservationViewModel()
        {
            Code = model.Code,
            ReservationMethod = model.ReservationMethod,
            RoomNumber = model.RoomNumber,
            Floor = model.RoomNumberNavigation.Floor,
            RoomType = model.RoomNumberNavigation.RoomType,
            GuestUsername = model.GuestUsernameNavigation.Username,
            GuestFullName = model.GuestUsernameNavigation.FirstName + " " + model.GuestUsernameNavigation.MiddleName + " " + model.GuestUsernameNavigation.LastName,
            BookDate = model.BookDate.ToString("dd/MM/yyyy"),
            CheckIn = model.CheckIn.ToString("dd/MM/yyyy"),
            CheckOut = model.CheckOut.ToString("dd/MM/yyyy"),
            Cost = model.RoomNumberNavigation.Cost,
            PaymentDate = model.PaymentDate?.ToString("dd/MM/yyyy"),
            PaymentMethod = model.PaymentMethod,
            Remark = model.Remark
        };
    }
}
