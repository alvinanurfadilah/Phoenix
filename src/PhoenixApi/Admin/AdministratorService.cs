using PhoenixApi.Admin;
using PhoenixBusiness.Exceptions;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixApi;

public class AdministratorService
{
    private readonly IAdministratorRepository _repository;

    public AdministratorService(IAdministratorRepository repository)
    {
        _repository = repository;
    }

    public ResponseDTO<AdminFormDTO> Get(string username)
    {
        var model = _repository.Get(username);

        return new ResponseDTO<AdminFormDTO>()
        {
            Message = ConstantMessage.SUCCESS_MESSAGE_GET,
            Status = ConstantMessage.SUCCESS_STATUS,
            Data = new AdminFormDTO()
            {
                Username = model.Username,
                Password = model.Password,
                JobTitle = model.JobTitle
            }
        };
    }

    public ResponseDTO<string?> Insert(AdminFormDTO dto)
    {
        string message;
        bool error = false;
        try
        {
            var model = new Administrator()
            {
                Username = dto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                JobTitle = dto.JobTitle
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

    public ResponseDTO<string?> Update(AdminFormDTO dto)
    {
        string message;
        bool error = false;

        try
        {
            var model = _repository.Get(dto.Username);
            model.JobTitle = dto.JobTitle;

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
