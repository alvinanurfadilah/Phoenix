using PhoenixBusiness.Interfaces;

namespace PhoenixApi.RoomService;

public class RoomServiceService
{
    private readonly IRoomServiceRepository _repository;

    public RoomServiceService(IRoomServiceRepository repository)
    {
        _repository = repository;
    }

    public ResponseDTO<RoomServiceFormDTO> Get(string employeeNumber)
    {
        var model = _repository.Get(employeeNumber);

        return new ResponseDTO<RoomServiceFormDTO>()
        {
            Message = ConstantMessage.SUCCESS_MESSAGE_GET,
            Status = ConstantMessage.SUCCESS_STATUS,
            Data = new RoomServiceFormDTO()
            {
                EmployeeNumber = model.EmployeeNumber,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                OutsourcingCompany = model.OutsourcingCompany
            }
        };
    }

    public ResponseDTO<string?> Insert(RoomServiceFormDTO dto)
    {
        string message;
        bool error = false;

        try
        {
            var model = new PhoenixDataAccess.Models.RoomService()
            {
                EmployeeNumber = dto.EmployeeNumber,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                OutsourcingCompany = dto.OutsourcingCompany
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

    public ResponseDTO<string?> Update(RoomServiceFormDTO dto)
    {
        string message;
        bool error = false;

        try
        {
            var model = _repository.Get(dto.EmployeeNumber);
            model.FirstName = dto.FirstName;
            model.MiddleName = dto.MiddleName;
            model.LastName = dto.LastName;
            model.OutsourcingCompany = dto.OutsourcingCompany;

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
