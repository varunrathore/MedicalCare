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
                // Configure your DbContext here using the configuration
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ITestCategoryRepository, TestCategoryRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
        }
    }
}
