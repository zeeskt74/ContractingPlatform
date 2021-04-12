using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.Repositories
{
    public static class RepositoriesDI
    {
        public static void ConfigureRepos(this IServiceCollection services)
        {
            services.AddTransient<IContractorRepo, ContractorRepo>();
        }
    }
}
