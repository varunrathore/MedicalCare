using MedicalCare.Application.Interfaces;
using MedicalCare.Infrastructure.Persistence;
using MedicalCare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace MedicalCare.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services here
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // ASP.NET Core automatically binds environment variables with __ to configuration
                // ConnectionStrings__DefaultConnection environment variable maps to ConnectionStrings:DefaultConnection
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
                }
                
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ITestCategoryRepository, TestCategoryRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
        }
    }
}
