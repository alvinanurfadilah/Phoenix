using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhoenixApi.Account;
using PhoenixApi.Inventory;
using PhoenixApi.Reservation;
using PhoenixApi.RoomInventory;
using PhoenixApi.RoomService;
using PhoenixBusiness.Interfaces;
using PhoenixBusiness.Repositories;
using static PhoenixDataAccess.Dependencies;

namespace PhoenixApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IConfiguration configuration = builder.Configuration;
        IServiceCollection services = builder.Services;
        ConfigureService(configuration, services);
        services.AddControllers();

        services.AddScoped<AccountService>();
        services.AddScoped<IAdministratorRepository, AdministratorRepository>();
        services.AddScoped<AdministratorService>();
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<InventoryService>();
        services.AddScoped<IRoomInventoryRepository, RoomInventoryRepository>();
        services.AddScoped<RoomInventoryService>();
        services.AddScoped<IRoomServiceRepository, RoomServiceRepository>();
        services.AddScoped<RoomServiceService>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ReservationService>();

        builder.Services.AddCors(option => {
            option.AddPolicy(name: "AllowFrontEnd", builder => {
                builder.WithOrigins("http://localhost:5234")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)
                ),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Phoenix Api"
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Description = "Example value = Bearer token",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            // options.OperationAsyncFilter<SecurityRequirementsOperationFilter>();
        });

        var app = builder.Build();
        app.UseCors("AllowFrontEnd");
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
