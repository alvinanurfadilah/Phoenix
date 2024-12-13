using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixBusiness.Interfaces;
using PhoenixWeb.ViewModels.Booking;
using static PhoenixWeb.ViewModels.Constant;

namespace PhoenixWeb.Services;

public class BookingService
{
    private readonly IRoomRepository _repository;

    public BookingService(IRoomRepository repository)
    {
        _repository = repository;
    }

    private List<SelectListItem> DropdownType()
    {
        return new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = "RS",
                Text = "RS"
            },
            new SelectListItem()
            {
                Value = "RD",
                Text = "RD"
            },
            new SelectListItem()
            {
                Value = "RT",
                Text = "RT"
            },
            new SelectListItem()
            {
                Value = "VS",
                Text = "VS"
            },
            new SelectListItem()
            {
                Value = "VD",
                Text = "VD"
            },
            new SelectListItem()
            {
                Value = "VT",
                Text = "VT"
            }
        };
    }

    public BookingIndexViewModel Get(string number, string type, int pageNumber)
    {
        var model = _repository.Get(number, type, pageNumber, PageSize)
        .Select(booking => new BookingViewModel()
        {
            Number = booking.Number,
            Floor = booking.Floor,
            RoomType = booking.RoomType,
            GuestLimit = booking.GuestLimit,
            Cost = booking.Cost
        });

        return new BookingIndexViewModel()
        {
            Bookings = model.ToList(),
            Pagination = new ViewModels.PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count(number, type)
            },
            Number = number,
            SearchType = DropdownType()
        };
    }

    public BookingDetailViewModel Get(string number)
    {
        var model = _repository.Get(number);

        return new BookingDetailViewModel()
        {
            Number = model.Number,
            Floor = model.Floor,
            RoomType = model.RoomType,
            GuestLimit = model.GuestLimit,
            Cost = model.Cost,
            Description = model.Description
        };
    }
}
