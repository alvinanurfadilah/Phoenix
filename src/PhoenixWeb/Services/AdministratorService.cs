using PhoenixBusiness.Interfaces;
using PhoenixWeb.ViewModels.Admin;
using static PhoenixWeb.ViewModels.Constant;

namespace PhoenixWeb.Services;

public class AdministratorService
{
    private readonly IAdministratorRepository _repository;

    public AdministratorService(IAdministratorRepository repository)
    {
        _repository = repository;
    }

    public AdminIndexViewModel Get(int pageNumber)
    {
        var model = _repository.Get(pageNumber, PageSize)
        .Select(admin => new AdminViewModel()
        {
            Username = admin.Username,
            JobTitle = admin.JobTitle
        });

        return new AdminIndexViewModel()
        {
            Admins = model.ToList(),
            Pagination = new ViewModels.PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count()
            }
        };
    }
}
