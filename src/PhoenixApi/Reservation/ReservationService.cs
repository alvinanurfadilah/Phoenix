using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixBusiness.Interfaces;

namespace PhoenixApi.Reservation;

public class ReservationService
{
    private readonly IReservationRepository _repository;

    public ReservationService(IReservationRepository repository)
    {
        _repository = repository;
    }

    private List<SelectListItem> GetMonth()
    {
        List<SelectListItem> getMonth = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = "1",
                Text = "January"
            },
            new SelectListItem()
            {
                Value = "2",
                Text = "February"
            },
            new SelectListItem()
            {
                Value = "3",
                Text = "March"
            },
            new SelectListItem()
            {
                Value = "4",
                Text = "April"
            },
            new SelectListItem()
            {
                Value = "5",
                Text = "May"
            },
            new SelectListItem()
            {
                Value = "6",
                Text = "June"
            },
            new SelectListItem()
            {
                Value = "7",
                Text = "July"
            },
            new SelectListItem()
            {
                Value = "8",
                Text = "August"
            },
            new SelectListItem()
            {
                Value = "9",
                Text = "September"
            },
            new SelectListItem()
            {
                Value = "10",
                Text = "October"
            },
            new SelectListItem()
            {
                Value = "11",
                Text = "November"
            },
            new SelectListItem()
            {
                Value = "12",
                Text = "December"
            }
        };

        return getMonth;
    }

    private List<int> GetDropdownYear()
    {
        var model = _repository.GetYear();

        int minYear = int.MaxValue;
        foreach (var item in model)
        {
            if (item < minYear)
            {
                minYear = item;
            }
        }

        int currentYear = DateTime.Today.Year;

        var dropdownYear = new List<int>();
        for (int i = minYear; i < currentYear + 5; i++)
        {
            dropdownYear.Add(i);
        }

        return dropdownYear;
    }

    private List<SelectListItem> GetYear()
    {
        var model = GetDropdownYear();
        return model.Select(year => new SelectListItem()
        {
            Text = year.ToString(),
            Value = year.ToString()
        }).ToList();
    }

    public ReservationDTO Get()
    {
        return new ReservationDTO()
        {
            GetMonth = GetMonth(),
            GetYear = GetYear()
        };
    }

    public ReservationTotalIncomeDTO TotalIncome(int month, int year)
    {
        var totalIncome = _repository.TotalIncome(month, year);
        return new ReservationTotalIncomeDTO()
        {
            TotalIncome = totalIncome.ToString("C", new CultureInfo("id-ID"))
        };
    }
}
