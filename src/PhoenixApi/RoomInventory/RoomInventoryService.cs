using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixApi.RoomInventory;

public class RoomInventoryService
{
    private readonly IRoomInventoryRepository _repository;
    private readonly IInventoryRepository _inventoryRepository;

    public RoomInventoryService(IRoomInventoryRepository repository, IInventoryRepository inventoryRepository)
    {
        _repository = repository;
        _inventoryRepository = inventoryRepository;
    }

    private List<SelectListItem> GetInventories()
    {
        var model = _inventoryRepository.Get()
        .Select(inv => new SelectListItem()
        {
            Text = inv.Name,
            Value = inv.Name
        }).ToList();

        return model;
    }

    public RoomInventoryFormDTO Get()
    {
        return new RoomInventoryFormDTO()
        {
            Inventories = GetInventories()
        };
    }

    public ResponseDTO<string?> Insert(RoomInventoryFormDTO dto)
    {
        string message;
        bool error = false;

        try
        {
            var model = new PhoenixDataAccess.Models.RoomInventory()
            {
                RoomNumber = dto.RoomNumber,
                InventoryName = dto.InventoryName,
                Quantity = dto.Quantity
            };

            _repository.Insert(model);
            message = ConstantMessage.SUCCESS_MESSAGE_POST;
        }
        catch (System.Exception exception)
        {
            error = true;
            message = exception.Message;
        }
        
        return new ResponseDTO<string?>()
        {
            Message = message,
            Status = error == false ? ConstantMessage.SUCCESS_STATUS : ConstantMessage.FAILED_STATUS,
            Data = null
        };
    }
}
