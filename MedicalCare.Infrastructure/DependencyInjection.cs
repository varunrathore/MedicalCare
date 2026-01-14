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
                // Try environment variable first (Azure), then fall back to appsettings.json for local development
                var connectionString = Environment.GetEnvironmentVariable("DefaultConnection")
                    ?? configuration.GetConnectionString("DefaultConnection");
                
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ITestCategoryRepository, TestCategoryRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
        }
    }
}
