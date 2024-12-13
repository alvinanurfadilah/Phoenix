using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PhoenixBusiness.Exceptions;
using PhoenixBusiness.Interfaces;
using PhoenixDataAccess.Models;

namespace PhoenixApi.Account;

public class AccountService
{
    private readonly IAdministratorRepository _adminRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IConfiguration _configuration;

    public AccountService(IAdministratorRepository adminRepository, IGuestRepository guestRepository, IConfiguration configuration)
    {
        _adminRepository = adminRepository;
        _guestRepository = guestRepository;
        _configuration = configuration;
    }

    private string CreateToken(AccountRequestDTO dto)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, dto.Username),
            new Claim(ClaimTypes.Role, dto.Role),
            // new Claim("JobTitle", dto.JobTitle)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value
            )
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return serializedToken;
        // return new AccountResponseDTO()
        // {
        //     Token = serializedToken
        // };
    }

    public string GetToken(AccountRequestDTO request)
    {
        var modelAdmin = new Administrator();
        var modelGuest = new Guest();

        if (request.Role == "Admin")
        {
            modelAdmin = _adminRepository.Get(request.Username);
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(request.Password, modelAdmin.Password);
            string role = request.Role;
            if (isCorrectPassword)
            {
                return CreateToken(request);
            }
        } else
        {
            modelGuest = _guestRepository.Get(request.Username);
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(request.Password, modelGuest.Password);
            string role = request.Role;
            if (isCorrectPassword)
            {
                return CreateToken(request);
            }
        }

        throw new LoginException("Username or Password or Role is incorrect");
    }


    public void ChangePassword(AccountChangePasswordDTO dto)
    {
        var modelAdmin = new Administrator();
        var modelGuest = new Guest();

        if (dto.Role == "Admin")
        {
            modelAdmin = _adminRepository.Get(dto.Username);
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(dto.OldPassword, modelAdmin.Password);
            if (!isCorrectPassword)
            {
                throw new LoginException("Password is incorrect!");
            }

            modelAdmin.Username = dto.Username;
            modelAdmin.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _adminRepository.ChangePassword(modelAdmin);
        } else
        {
            modelGuest = _guestRepository.Get(dto.Username);
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(dto.OldPassword, modelGuest.Password);
            if (!isCorrectPassword)
            {
                throw new LoginException("Password is incorrect!");
            }

            modelGuest.Username = dto.Username;
            modelGuest.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _guestRepository.ChangePassword(modelGuest);
        }
    }
}
