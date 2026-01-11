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

        public DbSet<TestCategory> TestCategories { get; set; } = null!;
        public DbSet<Test> Tests { get; set; } = null!;

    }
}
