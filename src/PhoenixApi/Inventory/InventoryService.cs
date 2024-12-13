using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixApi.Inventory;

public class InventoryService
{
    private readonly IInventoryRepository _repository;

    public InventoryService(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public ResponseDTO<InventoryFormDTO> Get(string name)
    {
        var model = _repository.Get(name);

        return new ResponseDTO<InventoryFormDTO>()
        {
            Message = ConstantMessage.SUCCESS_MESSAGE_GET,
            Status = ConstantMessage.SUCCESS_STATUS,
            Data = new InventoryFormDTO()
            {
                Name = model.Name,
                Description = model.Description,
                Stock = model.Stock
            }
        };
    }

    public ResponseDTO<string?> Insert(InventoryFormDTO dto)
    {
        string message;
        bool error = false;
        try
        {
            var model = new PhoenixDataAccess.Models.Inventory()
            {
                Name = dto.Name,
                Description = dto.Description,
                Stock = dto.Stock
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

    public ResponseDTO<string?> Update(InventoryFormDTO dto)
    {
        string message;
        bool error = false;

        try
        {
            var model = _repository.Get(dto.Name);
            model.Stock = dto.Stock;
            model.Description = dto.Description;

            _repository.Update(model);
            message = ConstantMessage.SUCCESS_MESSAGE_PUT;
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
