using PhoenixBusiness.Interfaces;
using PhoenixWeb.ViewModels.RoomService;
using static PhoenixWeb.ViewModels.Constant;

namespace PhoenixWeb.Services;

public class RoomServiceService
{
    private readonly IRoomServiceRepository _repository;

    public RoomServiceService(IRoomServiceRepository repository)
    {
        _repository = repository;
    }

    public RoomServiceIndexViewModel Get(string employeeNumber, string fullName, int pageNumber)
    {
        var model = _repository.Get(employeeNumber, fullName, pageNumber, PageSize)
        .Select(service => new RoomServiceViewModel()
        {
            EmployeeNumber = service.EmployeeNumber,
            FullName = service.FirstName + " " + service.MiddleName + " " + service.LastName,
            OutsourcingCompany = service.OutsourcingCompany
        });

        return new RoomServiceIndexViewModel()
        {
            RoomServices = model.ToList(),
            Pagination = new ViewModels.PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count(employeeNumber, fullName)
            },
            EmployeeNumber = employeeNumber,
            FullName = fullName
        };
    }

    public RoomServiceViewModel Get(string employeeNumber)
    {
        var model = _repository.Get(employeeNumber);
        return new RoomServiceViewModel()
        {
            EmployeeNumber = model.EmployeeNumber,
            FullName = model.FirstName + " " + model.MiddleName + " " + model.LastName,
            OutsourcingCompany = model.OutsourcingCompany,
            MondayRosterStart = model.MondayRosterStart == null ? "Start" : model.MondayRosterStart.ToString(),
            MondayRosterFinish = model.MondayRosterFinish == null ? "Finish" : model.MondayRosterFinish.ToString(),
            TuesdayRosterStart = model.TuesdayRosterStart == null ? "Start" : model.TuesdayRosterStart.ToString(),
            TuesdayRosterFinish = model.TuesdayRosterFinish == null ? "Finish" : model.TuesdayRosterFinish.ToString(),
            WednesdayRosterStart = model.WednesdayRosterStart == null ? "Start" : model.WednesdayRosterStart.ToString(),
            WednesdayRosterFinish = model.WednesdayRosterFinish == null ? "Finish" : model.WednesdayRosterFinish.ToString(),
            ThursdayRosterStart = model.ThursdayRosterStart == null ? "Start" : model.ThursdayRosterStart.ToString(),
            ThursdayRosterFinish = model.ThursdayRosterFinish == null ? "Finish" : model.ThursdayRosterFinish.ToString(),
            FridayRosterStart = model.FridayRosterStart == null ? "Start" : model.FridayRosterStart.ToString(),
            FridayRosterFinish = model.FridayRosterFinish == null ? "Finish" : model.FridayRosterFinish.ToString(),
            SaturdayRosterStart = model.SaturdayRosterStart == null ? "Start" : model.SaturdayRosterStart.ToString(),
            SaturdayRosterFinish = model.SaturdayRosterFinish == null ? "Finish" : model.SaturdayRosterFinish.ToString(),
            SundayRosterStart = model.SundayRosterStart == null ? "Start" : model.SundayRosterStart.ToString(),
            SundayRosterFinish = model.SundayRosterFinish == null ? "Finish" : model.SundayRosterFinish.ToString()
        };   
    }

    public RoomServiceRosterUpdateViewModel GetRoster(string employeeNumber)
    {
        var model = _repository.Get(employeeNumber);
        return new RoomServiceRosterUpdateViewModel()
        {
            EmployeeNumber = model.EmployeeNumber,
            FirstName = model.FirstName,
            MiddleName = model.MiddleName,
            LastName = model.LastName,
            OutsourcingCompany = model.OutsourcingCompany,
            MondayRosterStart = model.MondayRosterStart,
            MondayRosterFinish = model.MondayRosterFinish,
            TuesdayRosterStart = model.TuesdayRosterStart,
            TuesdayRosterFinish = model.TuesdayRosterFinish,
            WednesdayRosterStart = model.WednesdayRosterStart,
            WednesdayRosterFinish = model.WednesdayRosterFinish,
            ThursdayRosterStart = model.ThursdayRosterStart,
            ThursdayRosterFinish = model.ThursdayRosterFinish,
            FridayRosterStart = model.FridayRosterStart,
            FridayRosterFinish = model.FridayRosterFinish,
            SaturdayRosterStart = model.SaturdayRosterStart,
            SaturdayRosterFinish = model.SaturdayRosterFinish,
            SundayRosterStart = model.SundayRosterStart,
            SundayRosterFinish = model.SundayRosterFinish
        };   
    }

    public void Update(RoomServiceRosterUpdateViewModel viewModel)
    {
        var model = _repository.Get(viewModel.EmployeeNumber);
        model.FirstName = viewModel.FirstName;
        model.MiddleName = viewModel.MiddleName;
        model.LastName = viewModel.LastName;
        model.OutsourcingCompany = viewModel.OutsourcingCompany;
        model.MondayRosterStart = viewModel.MondayRosterStart;
        model.MondayRosterFinish = viewModel.MondayRosterFinish;
        model.TuesdayRosterStart = viewModel.TuesdayRosterStart;
        model.TuesdayRosterFinish = viewModel.TuesdayRosterFinish;
        model.WednesdayRosterStart = viewModel.WednesdayRosterStart;
        model.WednesdayRosterFinish = viewModel.WednesdayRosterFinish;
        model.ThursdayRosterStart = viewModel.ThursdayRosterStart;
        model.ThursdayRosterFinish = viewModel.ThursdayRosterFinish;
        model.FridayRosterStart = viewModel.FridayRosterStart;
        model.FridayRosterFinish = viewModel.FridayRosterFinish;
        model.SaturdayRosterStart = viewModel.SaturdayRosterStart;
        model.SaturdayRosterFinish = viewModel.SaturdayRosterFinish;
        model.SundayRosterStart = viewModel.SundayRosterStart;
        model.SundayRosterFinish = viewModel.SundayRosterFinish;

        _repository.Update(model);
    }
}
