using Core.Domain.Shared.Contacts;
using Infrastructure.Shared.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            #region Repositories
            services.AddTransient(typeof(IFileManagementRepository), typeof(FileManagementRepository));
            //services.AddTransient(typeof(IClientService), typeof(ClientService));
            //services.AddTransient(typeof(ITaxService), typeof(TaxService));

            #endregion Repositories


        }
    }
}
