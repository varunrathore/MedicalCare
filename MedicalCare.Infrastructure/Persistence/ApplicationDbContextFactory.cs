using MedicalCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationDbContextFactory
    : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=MedicalCareDb;User Id=sa;Password=StrongPass@123;TrustServerCertificate=True;")
            .Options;

        return new ApplicationDbContext(options);
    }
}
