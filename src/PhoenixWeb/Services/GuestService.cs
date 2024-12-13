using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;
using PhoenixWeb.ViewModels.Guest;

namespace PhoenixWeb.Services;

public class GuestService
{
    private readonly IGuestRepository _repository;

    public GuestService(IGuestRepository repository)
    {
        _repository = repository;
    }

    private List<SelectListItem> GetGenders()
    {
        return new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Text = "Female",
                Value = "F" 
            },
            new SelectListItem()
            {
                Text = "Male",
                Value = "M" 
            }
        };
    }

    public GuestInsertViewModel GetRegister()
    {
        return new GuestInsertViewModel()
        {
            Genders = GetGenders()
        };
    }

    public void Register(GuestInsertViewModel viewModel)
    {
        var model = new Guest()
        {
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            FirstName = viewModel.FirstName,
            MiddleName = viewModel.MiddleName,
            LastName = viewModel.LastName,
            BirthDate = viewModel.BirthDate,
            Gender = viewModel.Gender,
            Citizenship = viewModel.Citizenship,
            IdNumber = viewModel.IdNumber
        };

        _repository.Insert(model);
    }
}
