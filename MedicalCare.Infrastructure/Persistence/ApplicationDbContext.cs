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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Price property with proper precision
            modelBuilder.Entity<Test>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
