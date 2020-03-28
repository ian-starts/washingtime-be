using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WashingTime.Infrastructure;
using WashingTime.Infrastructure.Fakers;
using WashingTime.Infrastructure.Seeds;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace WashingTime
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<WashingTimeContext>();
                context.Database.Migrate();
            }

            return host;
        }

        public static IHost ConnectToDatabase(this IHost host, int maxRetryCount, TimeSpan retryDelay)
        {
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetService<WashingTimeContext>();
                    var connected = false;
                    var retries = 0;
                    while (!connected)
                    {
                        try
                        {
                            context.Database.CanConnect();
                            connected = true;
                        }
                        catch (Exception e)
                        {
                            retries++;
                            if (retries > maxRetryCount)
                            {
                                throw e;
                            }

                            Task.Delay(retryDelay).Wait();
                        }
                    }
                }

                return host;
            }
        }

        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var env = services.GetService<IWebHostEnvironment>();
                var context = services.GetService<WashingTimeContext>();
                var configuration = services.GetService<IConfiguration>();

                if (configuration.GetValue<bool>("Database:Seed"))
                {
                    new WashingTimeSeeder(context, new WashingTimeFaker()).Seed();
                    Console.WriteLine("Database seeded");
                }
            }

            return host;
        }
    }
}