using System.ComponentModel.DataAnnotations;
using PhoenixDataAccess.Models;

namespace PhoenixWeb;

public class UniqueNumberRoomValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var dbContext = (PhoenixContext?)validationContext.GetService(typeof(PhoenixContext)) ?? throw new NullReferenceException("System Error!");

            var isExist = dbContext.Rooms.Any(
                room => room.Number == value.ToString()
            );

            if (isExist)
            {
                return new ValidationResult($"{value} already exist!");
            }
        }
        return ValidationResult.Success;
    }
}
