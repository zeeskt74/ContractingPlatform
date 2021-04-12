using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.SQLite
{
    public static class SQLiteDI
    {
        public static void ConfigureDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ICPDbContext>(options => options.UseSqlite(connectionString));
        }
    }
}
