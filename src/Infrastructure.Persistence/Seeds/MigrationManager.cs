using Infrastructure.Persistence.Context;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.Seeds
{
    public static class MigrationManager
    {
        public static IHost MigratePersistenceDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            try
            {
                appContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ". " + ex.Source);
                throw;
            }

            return host;
        }
    }
}
