using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoenixDataAccess.Models;

namespace PhoenixDataAccess;

public static class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<PhoenixContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("PhoenixConnection"))
        );
    }
}
