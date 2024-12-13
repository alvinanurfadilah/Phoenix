using PhoenixBusiness;
using PhoenixBusiness.Interfaces;
using PhoenixBusiness.Repositories;
using PhoenixWeb.BackendGateway;
using PhoenixWeb.Controllers;
using PhoenixWeb.Services;

namespace PhoenixWeb.Configuratons;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<AccountService>();
        services.AddScoped<AccountBackendGatewayService>();
        services.AddScoped<IAdministratorRepository, AdministratorRepository>();
        services.AddScoped<AdministratorService>();
        services.AddScoped<AdminBackendGatewayService>();
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<GuestService>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<InventoryService>();
        services.AddScoped<InventoryBackendGatewayService>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<RoomService>();
        services.AddScoped<IRoomInventoryRepository, RoomInventoryRepository>();
        services.AddScoped<IRoomServiceRepository, RoomServiceRepository>();
        services.AddScoped<RoomServiceService>();
        services.AddScoped<RoomServiceBackendGatewayService>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ReservationService>();
        services.AddScoped<ReservationBackendGatewayService>();
        services.AddScoped<BookingService>();
        return services;
    }
}
