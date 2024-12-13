using System.Security.Claims;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoenixBusiness.Exceptions;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;
using PhoenixWeb.BackendGateway;
using PhoenixWeb.ViewModels.Account;

namespace PhoenixWeb.Services;

public class AccountService
{
    private readonly IGuestRepository _guestRepository;
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IConfiguration _configuration;

    public AccountService(IGuestRepository guestRepository, IAdministratorRepository administratorRepository, IConfiguration configuration)
    {
        _guestRepository = guestRepository;
        _administratorRepository = administratorRepository;
        _configuration = configuration;
    }

    private List<SelectListItem> GetRoles()
    {
        return new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Text = "Admin",
                Value = "Admin" 
            },
            new SelectListItem()
            {
                Text = "Guest",
                Value = "Guest" 
            }
        };
    }

    public AccountLoginViewModel GetLogin()
    {
        return new AccountLoginViewModel()
        {
            Roles = GetRoles()
        };
    }

    public async Task<string> GetToken(AccountLoginViewModel viewModel)
    {
        var request = new AccountRequestDTO()
        {
            Username = viewModel.Username,
            Password = viewModel.Password,
            Role = viewModel.Role
        };

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        var response = await client.PostAsJsonAsync("/api/v1/account", request);
        // var token = string.Empty;
        AccountResponseDTO content = new AccountResponseDTO();
        content.Token = await response.Content.ReadAsStringAsync();

        // if (response.IsSuccessStatusCode)
        // {   
        //     content = await response.Content.ReadFromJsonAsync<AccountResponseDTO>();
        //     // token = content?.Token ?? string.Empty;
        // }

        return content.Token;
    }

    private  async Task<ClaimsPrincipal> GetPrincipal(AccountLoginViewModel viewModel)
    {
        var token = await GetToken(viewModel);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, viewModel.Username),
            new Claim(ClaimTypes.Role, viewModel.Role ?? string.Empty),
            new Claim("JobTitle", viewModel.JobTitle),
            new Claim("Token", token)
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

    private AuthenticationTicket GetTicket(ClaimsPrincipal principal)
    {
        AuthenticationProperties authenticationProperties = new AuthenticationProperties()
        {
            IssuedUtc = DateTime.Now,
            ExpiresUtc = DateTime.Now.AddMinutes(30),
            AllowRefresh = false
        };

        AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme);

        return authenticationTicket;
    }

    public async Task<AuthenticationTicket> SetLogin(AccountLoginViewModel viewModel)
    {
        var modelAdmin = new Administrator();
        var modelGuest = new Guest();
        string username, password, role, jobTitle;

        
        if (viewModel.Role == "Admin")
        {
            modelAdmin = _administratorRepository.Get(viewModel.Username);
            
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(viewModel.Password, modelAdmin.Password);
            if (!isCorrectPassword)
            {
                throw new LoginException("Username or Password or Role is invalid!");
            }
            username = modelAdmin.Username;
            password = viewModel.Password;
            role = viewModel.Role;
            jobTitle = modelAdmin.JobTitle;
        } else
        {
            modelGuest = _guestRepository.Get(viewModel.Username);
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(viewModel.Password, modelGuest.Password);
            if (!isCorrectPassword)
            {
                throw new LoginException("Username or Password or Role is invalid!");
            }
            username = modelGuest.Username;
            password = viewModel.Password;
            role = viewModel.Role;
            jobTitle = "";
        }

        viewModel = new AccountLoginViewModel()
        {
            Username = username,
            Password = password,
            Role = role,
            JobTitle = jobTitle
        };

        Task<ClaimsPrincipal> principal = GetPrincipal(viewModel);
        AuthenticationTicket ticket = GetTicket(await principal);

        return ticket;
    }
}
