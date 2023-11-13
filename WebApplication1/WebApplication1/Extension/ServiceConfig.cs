using IncomeBridgeAPI.Service.Implementation.Costomer.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace IncomeBridgeAPI.Extension
{
    public static class ServiceConfig
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                getAssembly => getAssembly.MigrationsAssembly(typeof(DBContext).Assembly.FullName)
            ));
        }
    }
}
