using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.ServiceConfigurations;

public static class DbConfigs
{
    public static IServiceCollection AddDbConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(configuration["DB_CONNECTION_STRING"]));
        
        
        return services;
    }
}