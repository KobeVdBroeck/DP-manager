using DP_manager_API.Data;
using Microsoft.EntityFrameworkCore;

namespace DP_manager_API.Configuration;

public static class ServiceExtensions
{
    public static void AddPostgresDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString), ServiceLifetime.Scoped);
    }
}
