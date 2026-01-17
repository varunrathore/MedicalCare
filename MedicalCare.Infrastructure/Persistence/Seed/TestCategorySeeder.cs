using MedicalCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace MedicalCare.Infrastructure.Persistence.Seed;

public class TestCategorySeeder
{
    private readonly ApplicationDbContext _context;
    public TestCategorySeeder(ApplicationDbContext context)
    {
        _context = context;
    }
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.TestCategories.AnyAsync())
            return;

        context.TestCategories.AddRange(
            new TestCategory
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Blood Tests",
                IsActive = true
            },
            new TestCategory
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Kidney Function (Renal)",
                IsActive = true
            },
            new TestCategory
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Infectious Diseases (COVID-19, Malaria, Dengue)",
                IsActive = true
            },
            new TestCategory
            {
                Id = Guid.Parse("44444444-4444-4444-4444-4444444444"),
                Name = "Liver Function (Hepatic)",
                IsActive = true
            },
            new TestCategory
            {
                Id = Guid.Parse("55555555-5555-5555-5555-5555555555"),
                Name = "Vitamin & Minerals",
                IsActive = true
            },
            new TestCategory
            {   
                Id = Guid.Parse("66666666-6666-6666-6666-6666666666"),
                Name = "Imaging/Radiology (X-Ray, Ultrasound)",
                IsActive = true
            },
            new TestCategory
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777"),
                Name = "Full Body Checkups (Packages)",
                IsActive = true
            },
            new TestCategory
            {
                Id = Guid.Parse("88888888-8888-8888-8888-88888888"),
                Name = "Blood Tests (Hematology)",
                IsActive = true
            },
            new TestCategory
            {
                Id = Guid.Parse("99999999-9999-9999-9999-99999999"),
                Name = "Diabetes Profile",
                IsActive = true
            },
            new TestCategory
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa"),
                Name = "Heart Care (Cardiology)",
                IsActive = true
            }
        );

        await context.SaveChangesAsync();
    }
}