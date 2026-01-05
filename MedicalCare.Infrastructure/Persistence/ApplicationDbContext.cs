using Microsoft.EntityFrameworkCore;
using System;
using MedicalCare.Domain.Entities;

namespace MedicalCare.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
// {
//     base.OnModelCreating(modelBuilder);

//     modelBuilder.Entity<TestCategory>().HasData(
//         new TestCategory
//         {
//             Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
//             Name = "Blood Tests",
//             IsActive = true
//         },
//         new TestCategory
//         {
//             Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
//             Name = "Diabetes Profile",
//             IsActive = true
//         },
//         new TestCategory
//         {
//             Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
//             Name = "Heart Care",
//             IsActive = true
//         },
//         new TestCategory
//         {
//             Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
//             Name = "Kidney Function",
//             IsActive = true
//         },
//         new TestCategory
//         {
//             Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
//             Name = "Liver Function",
//             IsActive = true
//         },
//         new TestCategory
//         {
//             Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
//             Name = "Thyroid Profile",
//             IsActive = true
//         },
//         new TestCategory
//         {
//             Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
//             Name = "Vitamin & Minerals",
//             IsActive = true
//         },
//         new TestCategory
//         {
//             Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
//             Name = "Imaging/Radiology",
//             IsActive = true
//         },
//         new TestCategory
//         {
//             Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
//             Name = "Infectious Diseases",
//             IsActive = true
//         },
//         new TestCategory
//         {
//             Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
//             Name = "Full Body Checkups",
//             IsActive = true
//         }
//     );
// }

        public DbSet<TestCategory> TestCategories { get; set; } = null!;
        public DbSet<Test> Tests { get; set; } = null!;

    }
}
