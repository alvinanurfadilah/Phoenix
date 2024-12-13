using System.ComponentModel.DataAnnotations;
using PhoenixDataAccess.Models;

namespace PhoenixApi.Validations;

public class UniqueUsernameAdminValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var dbContext = (PhoenixContext?)validationContext.GetService(typeof(PhoenixContext)) ?? throw new NullReferenceException("System Error!");

            var isExist = dbContext.Guests.Any(
                admin => admin.Username == value.ToString()
            );

            if (isExist)
            {
                return new ValidationResult($"{value} already exist!");
            }
        }
        return ValidationResult.Success;
    }
}
