using MedicalCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationDbContextFactory
    : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Read connection string from environment variable (Azure) with fallback to local default
        var connectionString = Environment.GetEnvironmentVariable("DefaultConnection")
            ?? "Server=localhost,1433;Database=MedicalCareDb;User Id=sa;Password=StrongPass@123;TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new ApplicationDbContext(options);
    }
}
