using MedicalCare.Application.Interfaces;
using MedicalCare.Domain.Entities;
using MedicalCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedicalCare.Infrastructure.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;
        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Test>> GetAllAsync()
        { 
            return await _context.Tests.ToListAsync();
        }
        public async Task<List<Test>> GetAllWithCategoryAsync()
        {
            return await _context.Tests
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<Test?> GetByIdAsync(Guid id)
        {
            return await _context.Tests.FindAsync(id);
        }
        public async Task<Test?> GetByNameAsync(string name)
        {
            return await _context.Tests.FirstOrDefaultAsync(t => t.Name == name);
        }
        public async Task AddAsync(Test test)
        {
            await _context.Tests.AddAsync(test);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Test test)
        {
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();
        }
    }
}
