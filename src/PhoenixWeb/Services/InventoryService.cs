using PhoenixBusiness.Interfaces;
using PhoenixWeb.ViewModels.Inventory;
using static PhoenixWeb.ViewModels.Constant;

namespace PhoenixWeb.Services;

public class InventoryService
{
    private readonly IInventoryRepository _repository;

    public InventoryService(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public InventoryIndexViewModel Get(int pageNumber)
    {
        var model = _repository.Get(pageNumber, PageSize)
        .Select(inv => new InventoryViewModel()
        {
            Name = inv.Name,
            Description = inv.Description,
            Stock = inv.Stock
        });

        return new InventoryIndexViewModel()
        {
            Inventories = model.ToList(),
            Pagination = new ViewModels.PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count()
            }
        };
    }

    public void Delete(string name)
    {
        var viewModel = _repository.Get(name);
        _repository.Delete(viewModel);
    }
}
