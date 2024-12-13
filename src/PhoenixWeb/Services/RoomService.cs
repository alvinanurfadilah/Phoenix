using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;
using PhoenixWeb.ViewModels;
using PhoenixWeb.ViewModels.Room;
using PhoenixWeb.ViewModels.RoomInventory;
using static PhoenixWeb.ViewModels.Constant;

namespace PhoenixWeb.Services;

public class RoomService
{
    private readonly IRoomRepository _repository;
    private readonly IRoomInventoryRepository _roomInventoryRepository;

    private readonly IReservationRepository _reservationRepository;

    public RoomService(IRoomRepository repository, IRoomInventoryRepository roomInventoryRepository, IReservationRepository reservationRepository)
    {
        _repository = repository;
        _roomInventoryRepository = roomInventoryRepository;
        _reservationRepository = reservationRepository;
    }

    private List<SelectListItem> DropdownStatus()
    {
        return new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = "Vacant",
                Text = "Vacant"
            },
            new SelectListItem()
            {
                Value = "Booked",
                Text = "Booked"
            }
        };
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

    public RoomIndexViewModel Get(string number, string type, int pageNumber)
    {
        var model = _repository.Get(number, type, pageNumber, PageSize)
        .Select(room => new RoomViewModel()
        {
            Number = room.Number,
            Floor = room.Floor,
            RoomType = room.RoomType,
            GuestLimit = room.GuestLimit,
            Cost = room.Cost
        });

        return new RoomIndexViewModel()
        {
            Rooms = model.ToList(),
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count(number, type)
            },
            Number = number,
            SearchType = DropdownType(),
            SearchStatus = DropdownStatus()
        };
    }

    public void Insert(RoomInsertViewModel viewModel)
    {
        var model = new Room()
        {
            Number = viewModel.Number,
            Floor = viewModel.Floor,
            RoomType = viewModel.RoomType,
            GuestLimit = viewModel.GuestLimit,
            Description = viewModel.Description,
            Cost = viewModel.Cost
        };

        _repository.Insert(model);
    }

    public RoomUpdateViewModel Get(string number)
    {
        var model = _repository.Get(number);
        return new RoomUpdateViewModel()
        {
            Number = model.Number,
            Floor = model.Floor,
            RoomType = model.RoomType,
            GuestLimit = model.GuestLimit,
            Description = model.Description,
            Cost = model.Cost
        };
    }

    public void Update(RoomUpdateViewModel viewModel)
    {
        var model = _repository.Get(viewModel.Number);
        model.Number = viewModel.Number;
        model.Floor = viewModel.Floor;
        model.RoomType = viewModel.RoomType;
        model.GuestLimit = viewModel.GuestLimit;
        model.Description = viewModel.Description;
        model.Cost = viewModel.Cost;

        _repository.Update(model);
    }

    public RoomInventoryItemViewModel GetRoomItem(string number)
    {
        var model = _repository.GetRoomItem(number);

        return new RoomInventoryItemViewModel()
        {
            RoomNumber = model.Number,
            Floor = model.Floor,
            RoomType = model.RoomType,
            GuestLimit = model.GuestLimit,
            RoomInventories = model.RoomInventories
            .Select(roominventory => new RoomInventoryViewModel()
            {
                Id = roominventory.Id,
                InventoryName = roominventory.InventoryName,
                Stock = roominventory.InventoryNameNavigation.Stock,
                Quantity = roominventory.Quantity
            }).ToList()
            // Pagination = new PaginationViewModel() {
            //     PageNumber = pageNumber,
            //     PageSize = PageSize,
            //     TotalRows = _repository.Count(number)
            // }
        };
    }

    public RoomInventoryViewModel GetRoomInventory(long id)
    {
        var getRoomInventory = _roomInventoryRepository.Get(id);
        return new RoomInventoryViewModel()
        {
            Id = getRoomInventory.Id,
            RoomNUmber = getRoomInventory.RoomNumber
        };
    }

    public void DeleteRoomInventory(long id)
    {
        
        var viewModel = _roomInventoryRepository.Get(id);
        _roomInventoryRepository.Delete(viewModel);
    }


    public MyRoomIndexViewModel Get(string roomNumber, string guestUsername)
    {
        var model = _reservationRepository.Get(roomNumber, guestUsername)
        .Select(res => new MyRoomViewModel()
        {
            Code = res.Code,
            ReservationMethod = res.ReservationMethod,
            RoomNumber = res.RoomNumber,
            Floor = res.RoomNumberNavigation.Floor,
            RoomType = res.RoomNumberNavigation.RoomType,
            GuestUsername = res.GuestUsernameNavigation.Username,
            GuestFullName = res.GuestUsernameNavigation.FirstName + " " + res.GuestUsernameNavigation.MiddleName + " " + res.GuestUsernameNavigation.LastName,
            BookDate = res.BookDate.ToString("dd/MM/yyyy"),
            CheckIn = res.CheckIn.ToString("dd/MM/yyyy"),
            CheckOut = res.CheckOut.ToString("dd/MM/yyyy"),
            Cost = res.RoomNumberNavigation.Cost,
            PaymentDate = res.PaymentDate?.ToString("dd/MM/yyyy"),
            PaymentMethod = res.PaymentMethod,
            Remark = res.Remark
        }).ToList();

        return new MyRoomIndexViewModel()
        {
            MyRoom = model,
            RoomNumber = roomNumber
        };
    }
}
