using System.ComponentModel.DataAnnotations;
using PhoenixDataAccess.Models;

namespace PhoenixWeb.Validations;

public class UniqueUsernameGuestValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var dbContext = (PhoenixContext?)validationContext.GetService(typeof(PhoenixContext)) ?? throw new NullReferenceException("System Error!");

            var isExist = dbContext.Guests.Any(
                guest => guest.Username == value.ToString()
            );

            if (isExist)
            {
                return new ValidationResult($"{value} already exist!");
            }
        }
        return ValidationResult.Success;
    }
}
