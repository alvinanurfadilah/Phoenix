using System.ComponentModel.DataAnnotations;
using PhoenixDataAccess.Models;
using PhoenixWeb.ViewModels.Guest;

namespace PhoenixWeb.Validations;

public class UniqueIdNumberGuestValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var dbContext = (PhoenixContext?)validationContext.GetService(typeof(PhoenixContext)) ?? throw new NullReferenceException("System Error!");

            var username = ((GuestInsertViewModel)validationContext.ObjectInstance).Username;
            var isExist = dbContext.Guests.Any(
                guest => guest.IdNumber == value.ToString() && guest.Username != username
            );

            if (isExist)
            {
                return new ValidationResult($"{value} already exist!");
            }
        }
        return ValidationResult.Success;
    }
}
