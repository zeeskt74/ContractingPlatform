using ICP.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.Repositories
{
    public static class ServicesDI
    {
        public static void ConfigureICPServices(this IServiceCollection services)
        {
            services.AddScoped<IContractorService, ContractorService>();
            services.AddScoped<IContractService, ContractService>();
        }
    }
}
