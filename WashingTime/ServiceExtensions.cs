using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WashingTime.Infrastructure;

namespace WashingTime
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPostgreSqlHeldNodigContext(
            this IServiceCollection services,
            string queryString,
            string migrationsAssemblyName,
            bool enableSensitiveDataLogging = false)
        {
            return services.AddDbContext<WashingTimeContext>(
                options =>
                {
                    options.EnableSensitiveDataLogging(enableSensitiveDataLogging);
                    options.UseNpgsql(
                        queryString,
                        parameters =>
                        {
                            parameters.EnableRetryOnFailure(10);
                            parameters
                                .MigrationsAssembly(migrationsAssemblyName);
                        });
                });
        }
    }
}