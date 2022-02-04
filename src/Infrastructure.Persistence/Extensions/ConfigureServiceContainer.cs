using Core.Domain.Persistence.Contracts;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.Extensions
{
    public static class ConfigureServiceContainer
    {
        public static void AddPersistenceDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(
                    configuration.GetConnectionString("PersistenceConnectionForMysqlDb"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }

        public static void AddPersistenceRepositories(this IServiceCollection services)
        {
            //services.AddTransient(typeof(IRepositoryAsync<>), typeof(FakeRepositoryAsync<>));
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IPersistenceUnitOfWork, PersistenceUnitOfWork>();
        }
    }
}
